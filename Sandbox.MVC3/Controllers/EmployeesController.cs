using System.Web.Mvc;
using Sandbox.MVC.ViewModels;

namespace Sandbox.MVC.Controllers
{
    public class EmployeesController : ApplicationController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var model = new EmployeeInputModel();
            return View(model);
        }

    }
}
