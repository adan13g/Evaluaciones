using Mysql_Asp.Classes;
using Mysql_Asp.Migrations;
using Mysql_Asp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mysql_Asp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModeloDatos, Configuration>()); AreaRegistration.RegisterAllAreas();

            CheckRolesAndSuperUser();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private void CheckRolesAndSuperUser()
        {
            UsersHelper.CheckRole("Admin");
            UsersHelper.CheckRole("User");
            UsersHelper.CheckRole("Alumno");

            UsersHelper.CheckSuperUser();
        }

    }
}
