using System;
using NUnit.Framework;
using Ninject;
using Ninject.Parameters;
using Sandbox.MVC3.Lib.Commands;
using Sandbox.MVC3.Lib.Queries;

namespace Sandbox.MVC3.Infrastructure
{
    [TestFixture]
    public class DependencyConfiguration_Tests
    {
        private DependencyConfiguration configuration;
        private IKernel kernel;

        [SetUp]
        public void before_each_test()
        {
            kernel = new StandardKernel();
            configuration = new DependencyConfiguration(kernel);
        }

        [Test]
        [TestCase(typeof(IClock), Result = true)]
        [TestCase(typeof(ICommandStore), Result = true)]
        [TestCase(typeof(IQueryStore), Result = true)]
        public bool can_resolve_type(Type type)
        {
            ConfigureKernel();
            var request = kernel.CreateRequest(type, x => true, new IParameter[] { }, false, false);
            bool canResolveRequest = kernel.CanResolve(request);
            return canResolveRequest;
        }

        private void ConfigureKernel()
        {
            configuration.Configure();
        }
    }
}
