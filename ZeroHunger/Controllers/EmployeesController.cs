using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Ast.Selectors;
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

        [HttpGet]
        public ActionResult CollectionHistory()
        {
            var db = new ZeroHungerEntities();
            return View(db.Histories.ToList());
        }

        [HttpGet]
        public ActionResult Orders()
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
        public ActionResult CollectOrders(int id)
        {
            var db = new ZeroHungerEntities();
            var orders = (from order in db.Orders
                          where order.Employee_id == id
                          select order).ToList();

            foreach (var order in orders.ToList())
            {
                if (order.Status == "Collected")
                    orders.Remove(order);
            }

            return View(orders);
        }
    }
}