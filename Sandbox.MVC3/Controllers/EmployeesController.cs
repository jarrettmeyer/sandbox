using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sandbox.MVC3.ViewModels;

namespace Sandbox.MVC3.Controllers
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
