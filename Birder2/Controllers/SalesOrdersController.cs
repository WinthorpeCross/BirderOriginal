using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Birder2.Data;
using Birder2.Models;
using Birder2.ViewModels;
using Newtonsoft.Json;

namespace Birder2.Controllers
{
    //<---- *****Move to a new branch when observation is setup with Knockout ***** ---->


    public class SalesOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        // GET: SalesOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesOrders.ToListAsync());
        }

        // GET: SalesOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders
                .SingleOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            //SalesOrderViewModel salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel()
            {
                SalesOrderId = salesOrder.SalesOrderId,
                CustomerName = salesOrder.CustomerName,
                PONumber = salesOrder.PONumber
            };
            salesOrderViewModel.MessageToClient = "I originated from the viewmodel, rather than the model.";

            //var model = JsonConvert.SerializeObject(salesOrderViewModel);
            return View(salesOrderViewModel);
        }

        // GET: SalesOrders/Create
        public IActionResult Create()
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.MessageToClient = "I originated from the viewmodel, rather than the model.";
            //salesOrderViewModel.ObjectState = ObjectState.Added;
            return View(salesOrderViewModel);
        }

        [HttpPost]
        public JsonResult Save([FromBody]SalesOrderViewModel salesOrderViewModel)
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.CustomerName = salesOrderViewModel.CustomerName;
            salesOrder.PONumber = salesOrderViewModel.PONumber;

            _context.SalesOrders.Add(salesOrder);
            _context.SaveChanges();
            salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            salesOrderViewModel.MessageToClient = string.Format("{0}’s sales order has been added to the database.", salesOrder.CustomerName);

            return Json(JsonConvert.SerializeObject(salesOrderViewModel));
        }

        // GET: SalesOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders
                .Include(n => n.SalesOrderItems)
                .SingleOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }
            //
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel()
            {
                SalesOrderId = salesOrder.SalesOrderId,
                CustomerName = salesOrder.CustomerName,
                PONumber = salesOrder.PONumber
            };
            foreach (SalesOrderItem salesOrderItem in salesOrder.SalesOrderItems)
            {
                SalesOrderItemViewModel salesOrderItemViewModel = new SalesOrderItemViewModel();
                salesOrderItemViewModel.SalesOrderItemId = salesOrderItem.SalesOrderItemId;
                salesOrderItemViewModel.ProductCode = salesOrderItem.ProductCode;
                salesOrderItemViewModel.Quantity = salesOrderItem.Quantity;
                salesOrderItemViewModel.UnitPrice = salesOrderItem.UnitPrice;

                //salesOrderItemViewModel.ObjectState = ObjectState.Unchanged;
                //salesOrderItemViewModel.RowVersion = salesOrderItem.RowVersion;

                salesOrderItemViewModel.SalesOrderId = salesOrder.SalesOrderId;

                salesOrderViewModel.SalesOrderItems.Add(salesOrderItemViewModel);
            }
            salesOrderViewModel.MessageToClient = string.Format("The original value of Customer Name is {0}.", salesOrderViewModel.CustomerName);

            return View(salesOrderViewModel);
        }

        // POST: SalesOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("SalesOrderId,CustomerName,PONumber")] SalesOrder salesOrder)
        public JsonResult Edit([FromBody]SalesOrderViewModel salesOrderViewModel)
        {
            if (salesOrderViewModel.SalesOrderId == 0)
            {
                return Json(""); // on edit function check for null id
                //return Json(new { newLocation = "/Sales/Index/" });
            }


            var salesOrder = _context.SalesOrders
                .SingleOrDefault(m => m.SalesOrderId == salesOrderViewModel.SalesOrderId);
            if (salesOrder == null)
            {
                return Json("");
                //return Json(new { newLocation = "/Sales/Index/" });
            }

            salesOrder.CustomerName = salesOrderViewModel.CustomerName;
            salesOrder.PONumber = salesOrderViewModel.PONumber;
            salesOrderViewModel.MessageToClient =
                string.Format("The new value of Cusomer Name is {0}.", salesOrderViewModel.CustomerName);

            _context.Update(salesOrder);
            _context.SaveChanges();

            return Json(JsonConvert.SerializeObject(salesOrderViewModel));
            //return Json(new { newLocation = "/Sales/Index/" });
        }
    

        // POST: SalesOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderId,CustomerName,PONumber")] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesOrder);
        }



        // GET: SalesOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders
                .SingleOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            return View(salesOrder);
        }

        // POST: SalesOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrder = await _context.SalesOrders.SingleOrDefaultAsync(m => m.SalesOrderId == id);
            _context.SalesOrders.Remove(salesOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderExists(int id)
        {
            return _context.SalesOrders.Any(e => e.SalesOrderId == id);
        }
    }
}


//return Json(salesOrderViewModel);
//return RedirectToAction(nameof(Index));
//return Json( new { salesOrderViewModel });

//return NotFound();
//return RedirectToAction("Index");
//return View("Index");
//return DoSomething();