using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreShared.Data;

namespace MVCProject.Infrastructure.Filters
{
    public class SessionTimeoutAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx.Session["cart"] == null)
            {
                UsersRepository.EmptySessionCart();
                return;
            }
        }
    }
}