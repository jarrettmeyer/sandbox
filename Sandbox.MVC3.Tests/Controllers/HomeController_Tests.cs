using System;
using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;

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
        private HomeController controller;

        [SetUp]
        public void before_each_test()
        {
            controller = new HomeController();
        }

        [Test]
        public void get_index_returns_view()
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
            (result as JsonResult).Data.Should().BeAssignableTo<DateTime>();
        }
    }
}
