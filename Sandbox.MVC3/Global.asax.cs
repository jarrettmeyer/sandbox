using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sandbox.MVC3.Infrastructure;

namespace Sandbox.MVC3
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            new RouteConfiguration(RouteTable.Routes).Configure();
            new GlobalFilterConfiguration(GlobalFilters.Filters).Configure();
        }
    }
}