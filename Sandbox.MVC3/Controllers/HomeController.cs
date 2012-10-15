using System;
using System.Web.Mvc;

namespace Sandbox.MVC3.Controllers
{
    public class HomeController : ApplicationController
    {
        private readonly IClock clock;

        public HomeController()
            : this(SystemClock.Instance)
        {            
        }

        public HomeController(IClock clock)
        {
            this.clock = clock;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServerTime()
        {
            var currentServerTime = DateTime.Now;
            return Json(currentServerTime);
        }
    }
}
