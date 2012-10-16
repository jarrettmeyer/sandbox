using Ninject;
using Ninject.Web.Common;
using Sandbox.MVC.Lib.Commands;
using Sandbox.MVC.Lib.Queries;

namespace Sandbox.MVC.Infrastructure
{
    public class DependencyConfiguration
    {
        private readonly IKernel kernel;

        public DependencyConfiguration(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public void Configure()
        {
            kernel.Bind<IClock>().ToConstant(SystemClock.Instance).InSingletonScope();
            kernel.Bind<ICommandStore>().To<CommandStoreImpl>().InRequestScope();
            kernel.Bind<IQueryStore>().To<QueryStoreImpl>().InRequestScope();
        }
    }
}