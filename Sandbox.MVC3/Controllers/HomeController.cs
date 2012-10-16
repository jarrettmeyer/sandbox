using System.Web.Mvc;
using Sandbox.MVC.ViewModels;

namespace Sandbox.MVC.Controllers
{
    public class HomeController : ApplicationController
    {
        private readonly IClock clock;

        public HomeController(IClock clock)
        {
            this.clock = clock;
        }

        public ActionResult GoHome()
        {
            // This is an example of a redirect.
            return RedirectToAction("Index");
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
