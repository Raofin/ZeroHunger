using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Database;

namespace ZeroHunger.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        /*public ActionResult Index()
        {
            return View();
        }*/

        [HttpGet]
        public ActionResult Order(int id)
        {
            var db = new ZeroHungerEntities();
            var model = (from request in db.Requests
                         where request.Id == id
                         select request).SingleOrDefault();

            ViewBag.Employees = db.Employees.ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult SendOrder(int id, int requestId)
        {
            var db = new ZeroHungerEntities();
            db.Orders.Add(new Order {
                Request_Id = requestId,
                Employee_id = id,
                Order_Date = DateTime.Now,
                Status = "Pending"
            });

            var request = db.Requests.SingleOrDefault(e => e.Id == requestId);
            if (request != null)
                request.Status = "Pending";

            db.SaveChanges();

            return RedirectToAction("ViewRequests/" + id, "Restaurants");
        }

        /*[HttpPost]
        public ActionResult Order(FormCollection form)
        {
            /*var db = new ZeroHungerEntities();
            db.Requests.Add(new Order {
                Request_Id = Convert.ToInt32(form["Employees"]),
                Employee_id = model["Employees"],
                Expiry_Date = model.Expiry_Date,
                Status = "Pending"
            });
            db.SaveChanges();#1#

            return RedirectToAction("Index", "Restaurants");
        }*/

    }
}