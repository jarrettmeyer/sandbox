using System;
using System.Web.Mvc;

namespace Sandbox.MVC3.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServerTime()
        {
            var currentServerTime = DateTime.Now;
            return View(currentServerTime);
        }
    }
}
