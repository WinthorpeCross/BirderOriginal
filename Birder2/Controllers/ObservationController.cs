using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Birder2.Models;
using Birder2.Services;
using Microsoft.AspNetCore.Authorization;
using Birder2.ViewModels;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Birder2.Controllers
{
    [Authorize]
    public class ObservationController : Controller
    {
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly IObservationRepository _observationRepository;
        private readonly IMachineClockDateTime _systemClock;

        public ObservationController(IApplicationUserAccessor userAccessor,
                                        IObservationRepository observationRepository,
                                            IMachineClockDateTime systemClock)
        {
            _userAccessor = userAccessor;
            _observationRepository = observationRepository;
            _systemClock = systemClock;
        }


        // GET: Observation
        public async Task<IActionResult> Index(bool showUserObservationsOnly, int page)
        {
            if (page == 0)
            {
                page = 1;
            }
            //
            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            //

            ObservationsIndexViewModel viewModel = new ObservationsIndexViewModel()
            {
                ShowUserObservationsOnly = showUserObservationsOnly
            };


            

            try
            {
                if (showUserObservationsOnly == true)
                {
                    viewModel.Observations = await _observationRepository.MyObservationsList(user.Id);
                    // set view title
                    return View(viewModel);
                }
                var t = _observationRepository.MyNetworkObservationsList(user.Id);
                var paged = _observationRepository.MyNetworkObservationsList(user.Id).GetPaged(page, 5);
                //set view title
                return View(paged);
                //return View(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: Observation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            var observation = await _observationRepository.GetObservationDetails(id);

            if (observation == null)
            {
                return NotFound();
            }
            return View(observation);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var model = new CreateObservationViewModel()
                {
                    Observation = new Observation() { ObservationDateTime = _systemClock.Now },
                    MessageToClient = string.Empty,
                    Birds = await _observationRepository.AllBirdsList(),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public async Task<IActionResult> Create([Bind("ObservationId,ObservationDateTime,Location,Note,BirdId,LocationLatitude,LocationLongitude")] Observation observation)
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Post([FromBody]CreateObservationViewModel viewModel)
        {
            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return Json(JsonConvert.SerializeObject(viewModel));

                //return Json(new { newLocation = "/Sales/Index/" });
            }

            if (viewModel.ObservedSpecies.Count == 0)
            {
                //ModelState.AddModelError("ObservredSpeciesCollection", "You must choose at least one species of bird.");
                //string errors = JsonConvert.SerializeObject(ModelState.Values
                //                .SelectMany(state => state.Errors)
                //                .Select(error => error.ErrorMessage));

                viewModel.IsModelStateValid = false;
                viewModel.MessageToClient = "You must choose at least one observed bird species.";

                //return Json(JsonConvert.SerializeObject(ModelState));
                return Json(JsonConvert.SerializeObject(viewModel));
            }

            //loop here to set the bird for earch observation?
            if (ModelState.IsValid)
            {

                //roll back in case any cannot be updated?
                foreach (ObservedSpeciesViewModel observedSpecies in viewModel.ObservedSpecies)
                {
                    //ToDo: use AutoMaper here...
                    try
                    {
                        Observation observationToAdd = new Observation();
                        observationToAdd.ObservationDateTime = viewModel.Observation.ObservationDateTime;
                        observationToAdd.LocationLatitude = viewModel.Observation.LocationLatitude;
                        observationToAdd.LocationLongitude = viewModel.Observation.LocationLongitude;
                        observationToAdd.NoteGeneral = viewModel.Observation.NoteGeneral;
                        observationToAdd.NoteHabitat = viewModel.Observation.NoteHabitat;
                        observationToAdd.NoteWeather = viewModel.Observation.NoteWeather;
                        observationToAdd.NoteAppearance = viewModel.Observation.NoteAppearance;
                        observationToAdd.NoteBehaviour = viewModel.Observation.NoteBehaviour;
                        observationToAdd.NoteVocalisation = viewModel.Observation.NoteVocalisation;

                        observationToAdd.ApplicationUser = user;

                        observationToAdd.CreationDate = _systemClock.Now;
                        observationToAdd.LastUpdateDate = _systemClock.Now;
                        observationToAdd.Bird = await _observationRepository.GetSelectedBird(observedSpecies.BirdId);
                        observationToAdd.Quantity = observedSpecies.Quantity;
                        await _observationRepository.AddObservation(observationToAdd);
                    }
                    catch
                    {
                        return Json(JsonConvert.SerializeObject(viewModel));
                        //return Json(new { newLocation = "/Sales/Index/" });
                    }
                }

                viewModel.IsModelStateValid = true;
                return Json(JsonConvert.SerializeObject(viewModel));
            }
            else
            {
                string errors = JsonConvert.SerializeObject(ModelState.Values
                                .SelectMany(state => state.Errors)
                               .Select(error => error.ErrorMessage));

                viewModel.IsModelStateValid = false;
                viewModel.MessageToClient = errors;
                return Json(JsonConvert.SerializeObject(viewModel));
                //return Json(JsonConvert.SerializeObject(ModelState));
            }
        }

        // GET: Observation/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var model = new EditObservationViewModel
                {
                    Birds = await _observationRepository.AllBirdsList(),
                    Observation = await _observationRepository.GetObservationDetails(id)
                };

                if (model.Observation == null)
                {
                    return NotFound();
                }
                return View(model);
            }
            catch
            {
                //ToDo: Logging / return user to create view, like below?
                return NotFound("could not edit the observation");
            }
        }

        // POST: Observation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObservationId,ObservationDateTime,Quantity,LocationLatitude,LocationLongitude," +
                                                                "NoteGeneral,NoteHabitat,NoteWeather,NoteAppearance,NoteBehaviour," +
                                                                    "NoteVocalisation,BirdId,ApplicationUserId")] Observation observation)
        {
            // ToDo: Look into this update method.

            if (id != observation.ObservationId)
            {
                return NotFound();
            }

            var user = await _userAccessor.GetUser();
            if (user.Id != observation.ApplicationUserId)
            {
                return RedirectToAction("Login", "Account");
            }
            
            if (ModelState.IsValid)
            {
                //ToDo: use AutoMaper here...
                try
                {
                    Observation observationEdited = await _observationRepository.GetObservationDetails(id);
                    observationEdited.ObservationDateTime = observation.ObservationDateTime;
                    observationEdited.LocationLatitude = observation.LocationLatitude;
                    observationEdited.LocationLongitude = observation.LocationLongitude;
                    observationEdited.NoteGeneral = observation.NoteGeneral;
                    observationEdited.NoteHabitat = observation.NoteHabitat;
                    observationEdited.NoteWeather = observation.NoteWeather;
                    observationEdited.NoteAppearance = observation.NoteAppearance;
                    observationEdited.NoteBehaviour = observation.NoteBehaviour;
                    observationEdited.NoteVocalisation = observation.NoteVocalisation;

                    observationEdited.ApplicationUser = user;

                    observationEdited.LastUpdateDate = _systemClock.Now;
                    observationEdited.Bird = await _observationRepository.GetSelectedBird(observation.BirdId);
                    observationEdited.Quantity = observation.Quantity;
                    await _observationRepository.UpdateObservation(observationEdited);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _observationRepository.ObservationExists(observation.ObservationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  //return to details view?
            }

            var model = new EditObservationViewModel
            {
                Birds = await _observationRepository.AllBirdsList(),
                Observation = await _observationRepository.GetObservationDetails(id)
            };

            if (model.Observation == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Observation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observation = await _observationRepository.GetObservationDetails(id);

            if (observation == null)
            {
                return NotFound();
            }

            return View(observation);
        }

        // POST: Observation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _observationRepository.DeleteObservation(id);
            }
            catch
            {
                //logging
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ListLife()
        {
            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = await _observationRepository.GetLifeList(user.Id);

            return View(model);
        }
    }
}