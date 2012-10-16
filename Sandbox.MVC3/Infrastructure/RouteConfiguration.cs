using System.Web.Mvc;
using System.Web.Routing;

namespace Sandbox.MVC.Infrastructure
{
    public class RouteConfiguration
    {
        private readonly RouteCollection routes;

        public RouteConfiguration(RouteCollection routes)
        {
            this.routes = routes;
        }

        public void Configure()
        {
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}