using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sandbox.MVC.Infrastructure
{
    public class ResourceRouteConfigurationBuilder
    {
        private string[] actions;
        private string[] actionsWithId;
        private string idFormat = @"\d+";
        private string idName = "id";
        private string resource;
        private RouteCollection routes;        

        /// <summary>
        /// Indicates that a resource should have the 7 default restful actions.
        /// <para>
        /// 1. GET     resources/index
        /// </para>        
        /// <para>
        /// 2. GET     resources/new
        /// </para>
        /// <para>
        /// 3. POST    resources/create
        /// </para>
        /// <para>
        /// 4. GET     resources/id/show
        /// </para>
        /// <para>
        /// 5. GET     resources/id/edit
        /// </para>
        /// <para>
        /// 6. POST    resources/id/update
        /// </para>
        /// <para>
        /// 7. DELETE  resources/id/delete
        /// </para>
        /// </summary>
        public ResourceRouteConfigurationBuilder HavingAllDefaultActions
        {
            get
            {
                actions = new[] { "index", "new", "create" };
                actionsWithId = new[] { "show", "edit", "update", "delete" };
                return this;
            }
        }

        /// <summary>
        /// Add routes to the route table.
        /// </summary>        
        public void AddRoutes(RouteCollection routes)
        {
            this.routes = routes;
            BuildRoutes();
        }

        public ResourceRouteConfigurationBuilder ForResource(string resource)
        {
            this.resource = resource.ToLower();
            return this;
        }

        /// <summary>
        /// What basic actions exist on the controller? The first action listed will 
        /// be considered the default action for this group.
        /// </summary>
        public ResourceRouteConfigurationBuilder HavingActions(params string[] actions)
        {
            this.actions = actions.Select(a => a.ToLower()).ToArray();
            return this;
        }

        /// <summary>
        /// What basic actions, requiring an ID, exist on the controller? The first
        /// action listed will be the default action for this group.
        /// </summary>
        public ResourceRouteConfigurationBuilder HavingActionsWithId(params string[] actions)
        {
            this.actionsWithId = actions.Select(a => a.ToLower()).ToArray();            
            return this;
        }

        /// <summary>
        /// Sets the ID format. By default, the ID is an integer.
        /// </summary>
        public ResourceRouteConfigurationBuilder IdFormat(string format)
        {
            this.idFormat = format;
            return this;
        }

        /// <summary>
        /// Sets the ID name. By default, the ID name is 'id'.
        /// </summary>
        public ResourceRouteConfigurationBuilder IdName(string name)
        {
            this.idName = name.ToLower();
            return this;
        }

        private void BuildRoutes()
        {
            BuildRoutesWithId();
            BuildRoutesWithoutId();
        }

        private void BuildRoutesWithId()
        {
            if (actionsWithId == null || actionsWithId.Length == 0)
                return;

            string routeName = string.Format("{0}-ActionsWithId", resource);
            string url = string.Format("{0}/{{{1}}}/{{action}}", resource, idName);
            string defaultAction = actionsWithId[0];

            var defaults = new RouteValueDictionary
            {
                { "controller", resource },
                { "action", defaultAction }
            };            
            var constraints = BuildConstraintsForActionWithId();

            routes.MapLowercaseRoute(routeName, url, defaults, constraints);
        }

        private RouteValueDictionary BuildConstraintsForActionWithId()
        {
            var constraints = new RouteValueDictionary();
            constraints.Add(idName, idFormat);
            constraints.Add("action", string.Join("|", actionsWithId));
            return constraints;
        }

        private void BuildRoutesWithoutId()
        {
            if (actionsWithId == null || actionsWithId.Length == 0)
                return;

            string routeName = string.Format("{0}-Actions", resource);
            string url = string.Format("{0}/{{action}}", resource);
            string defaultAction = actions[0];

            object defaults = new { controller = resource, action = defaultAction };
            object constraints = new { action = string.Join("|", actions) };

            routes.MapRoute(routeName, url, defaults, constraints);
        }
    }
}