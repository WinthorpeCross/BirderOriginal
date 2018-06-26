using Birder2.Extensions;
using Birder2.Models;
using Birder2.Services;
using Birder2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

//ToDo: Threading is expensive.  Refactor to use only when it is necessary.
//ToDo: Refactor and write more services!

namespace Birder2.Controllers
{
    [Authorize]
    public class ObservationController : Controller
    {
        private const int pageSize = 15;
        private readonly IObservationRepository _observationRepository;
        private readonly IMachineClockDateTime _systemClock;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly ILogger _logger;
        private readonly IObservationsAnalysisService _observationsAnalysisService;

        public ObservationController(IObservationsAnalysisService observationsAnalysisService,
                                        IApplicationUserAccessor userAccessor,
                                            IObservationRepository observationRepository,
                                                IMachineClockDateTime systemClock,
                                                    ILogger<Network> logger)
        {
            _observationRepository = observationRepository;
            _observationsAnalysisService = observationsAnalysisService;
            _userAccessor = userAccessor;
            _systemClock = systemClock;
            _logger = logger;
        }

        public async Task<IActionResult> Images(int id)
        {
            //Check if Observation exists?
            var observation = await _observationRepository.GetObservationDetails(id);
            var viewModel = new ManageImagesDto()
            {
                ObservationId = observation.ObservationId
            };
            
            return View(viewModel);
        }

        public async Task<IActionResult> Index(ObservationsFeedFilter filter, int page)
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
            var userName = await _userAccessor.GetUserName();
            //
            try
            {
                var viewModel = new ObservationsIndexViewModel()
                {
                    Filter = filter
                };

                switch (filter)
                {
                    case ObservationsFeedFilter.Users: // Only Users
                        viewModel.Observations = await _observationRepository.GetUsersObservationsList(user.Id).GetPaged(page, pageSize);
                        break;
                    case ObservationsFeedFilter.Public: // Public
                        viewModel.Observations = await _observationRepository.GetPublicObservationsList().GetPaged(page, pageSize);
                        break;
                    default:
                        viewModel.Observations = await _observationRepository.GetUsersNetworkObservationsList(user.Id).GetPaged(page, pageSize);
                        break;
                }

                if (viewModel.Observations.Results.Count == 0)
                {
                    viewModel.Observations = await _observationRepository.GetPublicObservationsList().GetPaged(page, pageSize);
                    viewModel.IsEmptyList = true;
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Observations Index() error");
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var user = await _userAccessor.GetUser();
                if (user == null)
                {
                    return NotFound(); //ToDo: Need to return an alternative here!
                }

                var birdsList = await _observationRepository.AllBirdsList();
                if (birdsList == null)
                {
                    return NotFound(); //ToDo: Need to return an alternative here!
                }

                var viewModel = new CreateObservationViewModel()
                {
                    Observation = new Observation() { ObservationDateTime = _systemClock.Now },
                    MessageToClient = string.Empty,
                    Birds = birdsList,
                    DefaultLatitude = user.DefaultLocationLatitude,
                    DefaultLongitude = user.DefaultLocationLongitude
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Create observation error");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateSingle()
        {
            try
            {
                var user = await _userAccessor.GetUser();
                if (user == null)
                {
                    return NotFound(); //ToDo: Need to return an alternative here!
                }

                var birdsList = await _observationRepository.AllBirdsList();
                if (birdsList == null)
                {
                    return NotFound(); //ToDo: Need to return an alternative here!
                }

                var viewModel = new CreateSingleObservationViewModel()
                {
                    Observation = new Observation() { ObservationDateTime = _systemClock.Now, Quantity = 1 },
                    MessageToClient = string.Empty,
                    Birds = birdsList,
                    DefaultLatitude = user.DefaultLocationLatitude,
                    DefaultLongitude = user.DefaultLocationLongitude
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Create single observation error");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateSingle([FromBody]CreateSingleObservationViewModel viewModel)
        {
            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return Json(JsonConvert.SerializeObject(viewModel));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //ToDo: use AutoMaper here...
                    Observation observationToAdd = new Observation();
                    observationToAdd.Bird = await _observationRepository.GetSelectedBird(viewModel.Observation.BirdId);
                    observationToAdd.ObservationDateTime = viewModel.Observation.ObservationDateTime.AddSeconds(01).AddMilliseconds(456);
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
                    observationToAdd.Quantity = viewModel.Observation.Quantity;

                    var addObservation = _observationRepository.AddObservation(observationToAdd);
                    addObservation.Wait();

                    //if(addObservation.IsCompletedSuccessfully == true)
                    //{
                    // --- upload images service
                    // (1) convert to byte[]
                    // (2) resize
                    // (3) upload to Blob storage

                    //addObservationTags
                    //};
                }
                catch
                {
                    return Json(JsonConvert.SerializeObject(viewModel));
                    //return Json(new { newLocation = "/Sales/Index/" });
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
            }

        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    try
                    {
                        //ToDo: use AutoMaper here...
                        Observation observationToAdd = new Observation();
                        observationToAdd.Bird = await _observationRepository.GetSelectedBird(observedSpecies.BirdId);
                        observationToAdd.ObservationDateTime = viewModel.Observation.ObservationDateTime.AddSeconds(01).AddMilliseconds(456);
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
                        observationToAdd.Quantity = observedSpecies.Quantity;

                        var addObservation = _observationRepository.AddObservation(observationToAdd);
                        addObservation.Wait();
                        
                        //if(addObservation.IsCompletedSuccessfully == true)
                        //{
                            // --- upload images service
                            // (1) convert to byte[]
                            // (2) resize
                            // (3) upload to Blob storage

                            //addObservationTags
                        //};
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
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return NotFound(); //ToDo: Need to return an alternative here!
            }

            var observation = await _observationRepository.GetObservationDetails(id);
            if (observation == null)
            {
                return NotFound(); //ToDo: Need to return an alternative here!
            }

            if (observation.ApplicationUserId != user.Id)
            {
                return NotFound(); //ToDo: Need to return an alternative here!
            }

            try
            {
                var model = new EditObservationViewModel
                {
                    Birds = await _observationRepository.AllBirdsList(),
                    Observation = observation
                };

                return View(model);
            }
            catch
            {
                //ToDo: Logging / return user to create view, like below?
                return NotFound("could not edit the observation");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userAccessor.GetUser();

            if (user == null)
            {
                return NotFound(); //ToDo: Need to return an alternative here!
            }

            var observation = await _observationRepository.GetObservationDetails(id);

            if (observation == null)
            {
                return NotFound();
            }

            var viewModel = new ObservationDetailsDto();
            viewModel.SelectedObservation = observation;

            if (observation.ApplicationUserId == user.Id)
            {
                viewModel.IsObservationOwner = true;
               
            }

            return View(viewModel);
        }

        // ToDo: Research overposting attacks.
        [HttpPost]
        public async Task<JsonResult> Edit([FromBody]EditObservationViewModel viewModel)
        {
            // ToDo: Look into this update method.

            //if (id != viewModel.Observation.ObservationId)
            //{
            //    viewModel.IsModelStateValid = false;
            //    viewModel.MessageToClient = "An error occurred (1).";

            //    //return Json(JsonConvert.SerializeObject(ModelState));
            //    return Json(JsonConvert.SerializeObject(viewModel));
            //}

            //
            //ToDo: this action should be accessible ONLY to the observation's owner...
            // check if editor is the same as the original.  Only the owner is allowed to edit their own observations.
            //
            var user = await _userAccessor.GetUser();
            if (user.Id != viewModel.Observation.ApplicationUserId)
            {
                viewModel.IsModelStateValid = false;
                viewModel.MessageToClient = "An error occurred (2).";

                //return Json(JsonConvert.SerializeObject(ModelState));
                return Json(JsonConvert.SerializeObject(viewModel));
            }

            if (ModelState.IsValid)
            {
                //ToDo: use AutoMaper here...
                try
                {
                    Observation observationEdited = await _observationRepository.GetObservationDetails(viewModel.Observation.ObservationId);
                    observationEdited.Bird = await _observationRepository.GetSelectedBird(viewModel.Observation.BirdId);
                    observationEdited.ObservationDateTime = viewModel.Observation.ObservationDateTime.AddSeconds(01).AddMilliseconds(456);
                    observationEdited.LocationLatitude = viewModel.Observation.LocationLatitude;
                    observationEdited.LocationLongitude = viewModel.Observation.LocationLongitude;
                    observationEdited.NoteGeneral = viewModel.Observation.NoteGeneral;
                    observationEdited.NoteHabitat = viewModel.Observation.NoteHabitat;
                    observationEdited.NoteWeather = viewModel.Observation.NoteWeather;
                    observationEdited.NoteAppearance = viewModel.Observation.NoteAppearance;
                    observationEdited.NoteBehaviour = viewModel.Observation.NoteBehaviour;
                    observationEdited.NoteVocalisation = viewModel.Observation.NoteVocalisation;

                    observationEdited.ApplicationUser = user;

                    observationEdited.LastUpdateDate = _systemClock.Now;

                    observationEdited.Quantity = viewModel.Observation.Quantity;
                    await _observationRepository.UpdateObservation(observationEdited);

                    viewModel.IsModelStateValid = true;
                    return Json(JsonConvert.SerializeObject(viewModel));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _observationRepository.ObservationExists(viewModel.Observation.ObservationId))
                    {
                        viewModel.IsModelStateValid = false;
                        viewModel.MessageToClient = "Concurrency error.";
                        return Json(JsonConvert.SerializeObject(viewModel));
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            viewModel.IsModelStateValid = false;
            viewModel.MessageToClient = "You must choose at least one observed bird species.";

            //return Json(JsonConvert.SerializeObject(ModelState));
            return Json(JsonConvert.SerializeObject(viewModel));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); //ToDo: Need to return an alternative here!
            }

            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return NotFound(); //ToDo: Need to return an alternative here!
            }

            var observation = await _observationRepository.GetObservationDetails(id);
            if (observation == null)
            {
                return NotFound(); //ToDo: Need to return an alternative here!
            }

            if (observation.ApplicationUserId != user.Id)
            {
                return NotFound(); //ToDo: Need to return an alternative here!
            }

            var viewModel = new DeleteObservationDto()
            {
                ObservationId = observation.ObservationId,
                ObservationDateTime = observation.ObservationDateTime,
                Quantity = observation.Quantity,
                HasPhotos = observation.HasPhotos,
                ObservedBird = observation.Bird
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var deleteObservation = _observationRepository.DeleteObservation(id);
                deleteObservation.Wait();
                if (deleteObservation.IsCompletedSuccessfully == true)
                {
                    // ToDo: Remove photographs from Blob storage.  Delete entire container.
                }
            }
            catch
            {
                //logging
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}