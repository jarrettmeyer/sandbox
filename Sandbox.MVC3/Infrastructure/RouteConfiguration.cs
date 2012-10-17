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
            // The most basic possible routing configuration
            new ResourceRouteConfigurationBuilder()
                .ForResource("Employees")
                .HavingAllDefaultActions
                .AddRoutes(routes);

            // Going crazy with specific actions, ID name, and ID format.
            new ResourceRouteConfigurationBuilder()
                .ForResource("Posts")
                .HavingActions("List")
                .HavingActionsWithId("Show")
                .IdName("title")
                .IdFormat(@"[a-z0-9\-]+")
                .AddRoutes(routes);

            // Specifically defining actions.
            new ResourceRouteConfigurationBuilder()
                .ForResource("Home")
                .HavingActions("Index", "GoHome", "ServerTime", "Routes")
                .AddRoutes(routes);

            routes.MapLowercaseRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}