﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sandbox.MVC.Infrastructure;

namespace Sandbox.MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            new RouteConfiguration(RouteTable.Routes).Configure();
            new GlobalFilterConfiguration(GlobalFilters.Filters).Configure();
        }
    }
}