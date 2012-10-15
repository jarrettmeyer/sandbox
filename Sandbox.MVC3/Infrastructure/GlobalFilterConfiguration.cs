using System.Web.Mvc;

namespace Sandbox.MVC3.Infrastructure
{
    public class GlobalFilterConfiguration
    {
        private readonly GlobalFilterCollection globalFilters;

        public GlobalFilterConfiguration(GlobalFilterCollection globalFilters)
        {
            this.globalFilters = globalFilters;
        }

        public void Configure()
        {
            globalFilters.Add(new HandleErrorAttribute());
        }
    }
}