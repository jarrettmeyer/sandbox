using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Sandbox.MVC.Lib.Commands;
using Sandbox.MVC.Lib.Queries;

namespace Sandbox.MVC.Controllers
{
    [TestFixture]
    public class ApplicationController_Tests
    {
        private ApplicationController controller;

        [SetUp]
        public void before_each_test()
        {
            var commandStore = new CommandStoreImpl(new List<ICommand> { new FakeCommand() });
            var queryStore = new QueryStoreImpl(new List<IQuery> { new FakeQuery() });

            controller = new FakeController();
            controller.CommandStore = commandStore;
            controller.QueryStore = queryStore;
        }

        [Test]
        public void can_get_requested_command()
        {
            var command = controller.GetCommand<FakeCommand>();
            command.Should().BeAssignableTo<FakeCommand>();
        }

        [Test]
        public void can_get_requested_query()
        {
            var query = controller.GetQuery<FakeQuery>();
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
