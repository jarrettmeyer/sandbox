using System.Web.Mvc;
using Ninject;
using Sandbox.MVC.Lib.Commands;
using Sandbox.MVC.Lib.Queries;

namespace Sandbox.MVC.Controllers
{
    public abstract class ApplicationController : Controller
    {
        [Inject]
        public ICommandStore CommandStore { get; set; }

        [Inject]
        public IQueryStore QueryStore { get; set; }

        /// <summary>
        /// Get a designated command.
        /// </summary>
        public TCommand GetCommand<TCommand>() where TCommand : ICommand
        {
            var command = CommandStore.GetCommand<TCommand>();
            return command;
        }

        /// <summary>
        /// Get a designated query.
        /// </summary>
        public TQuery GetQuery<TQuery>() where TQuery : IQuery
        {
            var query = QueryStore.GetQuery<TQuery>();
            return query;
        }
    }
}
