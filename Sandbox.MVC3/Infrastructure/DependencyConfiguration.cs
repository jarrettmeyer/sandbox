using Ninject;
using Sandbox.MVC3.Lib.Commands;
using Sandbox.MVC3.Lib.Queries;

namespace Sandbox.MVC3.Infrastructure
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
            kernel.Bind<ICommandStore>().To<CommandStoreImpl>();
            kernel.Bind<IQueryStore>().To<QueryStoreImpl>();
        }
    }
}