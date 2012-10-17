using System.Web.Routing;

namespace Sandbox.MVC.Infrastructure
{
    public class LowercaseRoute : Route
    {
        public LowercaseRoute(string url, IRouteHandler routeHandler) 
            : base(url, routeHandler)
        {
        }

        public LowercaseRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) 
            : base(url, defaults, routeHandler)
        {
        }

        public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) 
            : base(url, defaults, constraints, routeHandler)
        {
        }

        public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) 
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var virtualPath = base.GetVirtualPath(requestContext, values);
            if (virtualPath != null && virtualPath.VirtualPath != null)
            {
                virtualPath.VirtualPath = virtualPath.VirtualPath.ToLowerInvariant();
            }
            return virtualPath;
        }
    }
}