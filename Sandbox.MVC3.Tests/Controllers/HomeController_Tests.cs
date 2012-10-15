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
        public void get_index_returns_view()
        {
            var result = controller.Index();
            result.Should().BeAssignableTo<ViewResult>();
        }
    }
}
