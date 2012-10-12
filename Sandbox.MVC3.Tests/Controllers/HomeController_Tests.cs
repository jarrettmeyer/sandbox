using System;
using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;

namespace Sandbox.MVC3.Controllers
{
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
        public void get_index_should_return_view()
        {
            var result = controller.Index();
            result.Should().BeAssignableTo<ViewResult>();
        }

        [Test]
        public void get_servertime_should_return_vew()
        {
            var result = controller.ServerTime();
            result.Should().BeAssignableTo<ViewResult>();
        }

        [Test]
        public void get_servertime_data_should_be_datetime()
        {
            var result = controller.ServerTime();
            (result as ViewResult).Model.Should().BeAssignableTo<DateTime>();
        }
    }
}
