using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DOAN
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Session_Start()
        //{
        //    Session["username"] = null;
        //    Session["pid"] = "";
        //    Session["qty"] = "";
        //}

        //protected void Session_End()
        //{
        //    Session["username"] = null;
        //}

        protected void Session_Start()
        {
            Session["Cart"] = null;
            Session["UserLogin"] = null;
            Session["role"] = null;
            Session["UserAdmin"] = null;
        }
    }
}
