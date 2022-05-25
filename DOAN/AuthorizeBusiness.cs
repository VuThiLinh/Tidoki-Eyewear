using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN
{
    public class AuthorizeBusiness : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserAdmin"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/DangNhap/Index");
                return;
            }
            else
            {
                switch (HttpContext.Current.Session["role"].ToString())
                {
                    case Constant.ADMIN:
                    case Constant.EMPLOYEER:
                        return;
                    default:
                        filterContext.Result = new RedirectResult("/Admin/DangNhap/Index");
                        return;
                }
            }
        }
    }
}