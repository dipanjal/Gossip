using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Gossip.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(Session["user"]==null)
            {
                TempData["msg"] = "You Must Login First";
                filterContext.Result = RedirectToAction("Index","Login");
            }
        }
    }
}