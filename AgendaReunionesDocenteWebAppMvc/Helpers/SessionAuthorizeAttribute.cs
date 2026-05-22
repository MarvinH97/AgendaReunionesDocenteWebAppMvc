using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaReunionesDocenteWebAppMvc.Helpers
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["userId"] == null)
            {
                filterContext.Result = new RedirectResult("~/Usuarios/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}