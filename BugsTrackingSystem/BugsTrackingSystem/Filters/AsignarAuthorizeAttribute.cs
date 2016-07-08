using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BugsTrackingSystem.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AsignarAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.User.Identity;

            if (!user.IsAuthenticated)
            {
                return false;
            }

            return (string.IsNullOrEmpty(this.Users) || this.Users.Split(',').Any(x => x.Equals(user.Name))) &&
                   (string.IsNullOrEmpty(this.Roles) || this.Roles.Split(',').Any(x => httpContext.User.IsInRole(x)));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Account", action = "Login" })
                );
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Home", action = "NotAuthorized" })
                );
            }
        }

    }
}