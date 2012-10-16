using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Sandbox.MVC.Infrastructure;

namespace Sandbox.MVC4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);

            new GlobalFilterConfiguration(GlobalFilters.Filters).Configure();
            new RouteConfiguration(RouteTable.Routes).Configure();
        }
    }
}