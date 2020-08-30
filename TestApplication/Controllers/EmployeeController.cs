using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepo _employeeRepo;
        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEmployees()
        {
            return Json(_employeeRepo.GetEmployees(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployee(int id)
        {
            return Json(_employeeRepo.GetEmployee(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateEmployee(EmployeeDTO employee)
        {
            return Json(_employeeRepo.CreateEmployee(employee), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateEmployee(EmployeeDTO employee)
        {
            return Json(_employeeRepo.UpdateEmployee(employee), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEmployee(int id)
        {
            return Json(_employeeRepo.DeleteEmployee(id), JsonRequestBehavior.AllowGet);
        }
    }
}