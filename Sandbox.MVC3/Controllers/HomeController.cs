﻿using System.Web.Mvc;
using Sandbox.MVC3.ViewModels;

namespace Sandbox.MVC3.Controllers
{
    public class HomeController : ApplicationController
    {
        private readonly IClock clock;

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
            var model = new ServerInfoViewModel
            {
                Time = clock.Now
            };
            return Json(model);
        }
    }
}