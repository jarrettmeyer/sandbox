using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Sandbox.MVC3.Lib.Commands;
using Sandbox.MVC3.Lib.Queries;

namespace Sandbox.MVC3.Controllers
{
    [TestFixture]
    public class ApplicationController_Tests
    {
        private ApplicationController controller;

        [SetUp]
        public void before_each_test()
        {
            controller = new FakeController();
            controller.Commands = new List<ICommand> { new FakeCommand() };
            controller.Queries = new List<IQuery> { new FakeQuery() };
        }

        [Test]
        public void can_get_requested_command()
        {
            var command = controller.GetCommandFor<FakeCommand>();
            command.Should().BeAssignableTo<FakeCommand>();
        }

        [Test]
        public void can_get_requested_query()
        {
            var query = controller.GetQueryFor<FakeQuery>();
            query.Should().BeAssignableTo<FakeQuery>();
        }

        public class FakeController : ApplicationController { }

        public class FakeCommand : AbstractCommand
        {
            public override void ExecuteCommand() { }            
        }

        public class FakeQuery : AbstractQuery<int>
        {
            public override int ExecuteQuery()
            {
                return 1;
            }
        }
    }
}
