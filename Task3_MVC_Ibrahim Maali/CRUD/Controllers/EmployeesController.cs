using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = dbContext.Employees.AsNoTracking().ToList();
            return View(nameof(Index), employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var employeeDataBase=dbContext.Employees.FirstOrDefault(emp=> emp.Id ==id);
            //return Content($"{id}"); // used to make sure the id is received
            return View("Update", employeeDataBase);     
        }

        [HttpPost]
        public IActionResult Update(Employee employee )
        {
            //var employeeDataBase = dbContext.Employees.Find(id);
            //employeeDataBase.Name = employee.Name;
            //employeeDataBase.Email= employee.Email;
            //employeeDataBase.Password= employee.Password;
            //dbContext.SaveChanges();
            if (employee.Password is not null)
            {
                dbContext.Employees.Update(employee);
               
            }
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //var employeeDataBase = dbContext.Employees.FirstOrDefault(emp => emp.Id == id);
            var employeeDataBase = dbContext.Employees.Find(id);
            dbContext.Employees.Remove(employeeDataBase);
            dbContext.SaveChanges();
            //return Content($"{id}"); // used to make sure the id is received
            return RedirectToAction("Index");
        }
    }
}
