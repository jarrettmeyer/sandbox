using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Sandbox.MVC3.Lib.Commands;
using Sandbox.MVC3.Lib.Queries;

namespace Sandbox.MVC3.Controllers
{
    public abstract class ApplicationController : Controller
    {
        [Inject]
        public IEnumerable<ICommand> Commands { get; set; }

        [Inject]
        public IEnumerable<IQuery> Queries { get; set; }

        public TCommand GetCommandFor<TCommand>() where TCommand : ICommand
        {
            var command = Commands.First(c => c is TCommand);
            return (TCommand)command;
        }

        public TQuery GetQueryFor<TQuery>() where TQuery : IQuery
        {
            var query = Queries.First(q => q is TQuery);
            return (TQuery)query;
        }
    }
}
