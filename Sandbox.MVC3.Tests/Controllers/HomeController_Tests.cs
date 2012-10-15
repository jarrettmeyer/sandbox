using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using Sandbox.MVC3.ViewModels;

namespace Sandbox.MVC3.Controllers
{
    /// <summary>
    /// When testing a controller, we're only looking for 3 things:
    /// 
    /// 1. Is the action result the same as expected? (ViewResult, PartialViewResult,
    ///    RedirectToRouteResult, RedirectResult, etc.)
    /// 
    /// 2. Is the model type expected?
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
        public void get_index_should_return_view()
        {
            var result = controller.Index();
            result.Should().BeAssignableTo<ViewResult>();
        }

        [Test]
        public void get_servertime_should_return_vew()
        {
            var result = controller.ServerTime();
            result.Should().BeAssignableTo<JsonResult>();
        }

        [Test]
        public void get_servertime_data_should_be_datetime()
        {
            var result = controller.ServerTime();
            (result as JsonResult).Data.Should().BeAssignableTo<ServerInfoViewModel>();
        }
    }
}
