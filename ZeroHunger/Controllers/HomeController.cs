using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Database;

namespace ZeroHunger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Employees()
        {
            var db = new ZeroHungerEntities();
            return View(db.Employees.ToList());
        }

        [HttpGet]
        public ActionResult AddEmployees()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployees(Employee model)
        {
            var db = new ZeroHungerEntities();
            db.Employees.Add(model);
            db.SaveChanges();
            return RedirectToAction("Employees", "Home");
        }

        [HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            var employee = new Employee() { Id = id };

            using (var context = new ZeroHungerEntities())
            {
                context.Employees.Attach(employee);
                context.Employees.Remove(employee);
                context.SaveChanges();
            }

            return RedirectToAction("Employees", "Home");
        }

        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            var db = new ZeroHungerEntities();
            var model = (from employee in db.Employees
                         where employee.Id == id
                         select employee).SingleOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(Employee model)
        {
            using (var db = new ZeroHungerEntities())
            {
                var employee = db.Employees.SingleOrDefault(e => e.Id == model.Id);

                if (employee != null)
                {
                    employee.Name = model.Name;
                    employee.Email = model.Email;
                    employee.Age = model.Age;
                    employee.Sex = model.Sex;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Employees", "Home");
        }




    }
}