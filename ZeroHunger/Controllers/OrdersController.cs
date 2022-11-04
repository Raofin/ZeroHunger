using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Database;

namespace ZeroHunger.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new ZeroHungerEntities();
            var employees = (from employee in db.Employees.ToList()
                             from order in db.Orders.ToList()
                             where employee.Id == order.Employee_id && order.Status == "Pending"
                             select employee).ToList();

            /*var orders = db.Orders.ToList();
            var employeeModel = new List<Employee>(); 

            foreach (var employee in employees)
            {
                foreach (var order in orders)
                {
                    if (employee.Id == order.Employee_id && order.Status == "Pending")
                    {
                        employeeModel.Add(employee);
                    }
                }
            }*/

            return View(employees);
        }

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

            return RedirectToAction("ViewRequests/" + request.Restaurants_Id, "Restaurants");
        }

        [HttpGet]
        public ActionResult CollectOrders()
        {
            var db = new ZeroHungerEntities();
            /*var orders = (from order in db.Orders
                          where order.Employee_id == id
                          select order).ToList();*/
            var orders = db.Orders.ToList();

            foreach (var order in orders.ToList().Where(order => order.Status != "Pending"))
                orders.Remove(order);

            return View(orders);
        }

        [HttpGet]
        public ActionResult Collect(int id)
        {
            var db = new ZeroHungerEntities();
            var order = db.Orders.SingleOrDefault(o => o.Id == id);

            if (order == null)
                return RedirectToAction("", "Orders");
            
            var request = db.Requests.SingleOrDefault(r => r.Id == order.Request_Id);

            if (request == null)
                return RedirectToAction("", "Orders");

            order.Status = request.Status = "Collected";
            db.Histories.Add(new History {
                Order_Id = order.Id,
                Employee_Id = order.Employee_id,
                Restaurant_Id = request.Restaurants_Id,
                Collection_Time = DateTime.Now
            });

            db.SaveChanges();

            return RedirectToAction("CollectOrders/" + order.Employee_id, "Orders");
        }

        [HttpGet]
        public ActionResult History()
        {
            var db = new ZeroHungerEntities();
            return View(db.Histories.ToList());
        }
    }
}