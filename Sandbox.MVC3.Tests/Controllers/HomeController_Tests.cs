using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using Sandbox.MVC3.ViewModels;

namespace Sandbox.MVC3.Controllers
{
    /// <summary>
    /// When testing a controller, we're only looking for 2 things:
    /// 
    /// 1. Is the action result the same as expected? (ViewResult, PartialViewResult,
    ///    RedirectToRouteResult, RedirectResult, etc.)
    /// 
    /// 2. Is the correct type of model returned?
    /// </summary>
    [TestFixture]
    public class HomeController_Tests
    {
        private IClock clock;
        private HomeController controller;

        [SetUp]
        public void before_each_test()
        {
            clock = StubClock.FromDate(2012, 1, 1);
            controller = new HomeController(clock);
        }

        [Test]
        public void get_gohome_should_redirect_to_home()
        {
            // 1. Do we redirect?
            // 2. Do we go where we think we go?
            var result = controller.GoHome();
            result.Should().BeAssignableTo<RedirectToRouteResult>();
            (result as RedirectToRouteResult).RouteValues["action"].Should().Be("Index");            
        }

        [Test]
        public void get_index_should_return_view()
        {
            // 1. Do we return a view?
            var result = controller.Index();
            result.Should().BeAssignableTo<ViewResult>();
        }

        [Test]
        public void get_servertime_should_return_json()
        {
            // 1. Do we return JSON?
            // 2. Do we have the expected model type?
            var result = controller.ServerTime();
            result.Should().BeAssignableTo<JsonResult>();
            (result as JsonResult).Data.Should().BeAssignableTo<ServerInfoViewModel>();
        }
    }
}
