using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BugsTrackingSystem.Models;
using AsignarServices.Data;
using BugsTrackingSystem.Filters;

namespace BugsTrackingSystem.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly Lazy<AsignarDataService> _dataService = new Lazy<AsignarDataService>(() => new AsignarDataService());

        public AccountController()
        {

        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                int? userId = _dataService.Value.ValidateUser(model);
                if (userId != null)
                {
                    var userToken = new FormsAuthenticationTicket(1, userId.Value.ToString(), DateTime.Now,
                        model.RememberMe ? DateTime.Now.AddMonths(1) : DateTime.Now.AddMinutes(10),
                        model.RememberMe, _dataService.Value.GetRoleByUserId(userId.Value));
                    var headerToken = FormsAuthentication.Encrypt(userToken);

                    if (!string.IsNullOrEmpty(headerToken))
                    {
                        var authCookie = new HttpCookie("Auth", headerToken);
                        authCookie.Expires = model.RememberMe ? DateTime.Now.AddMonths(1) : DateTime.Now.AddMinutes(10);
                        Response.Cookies.Add(authCookie);
                        return RedirectToAction("Home", "Manage");
                    }
                    else
                    {
                        ModelState.AddModelError("", "failed to create token");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }

            return View(model);
        }

        [AsignarAuthenticate]
        [AsignarAuthorize]
        public ActionResult Logout()
        {
            Response.Cookies.Add(new HttpCookie("Auth", null));
            Session.Abandon();
            return RedirectToAction("Home", "Manage");
        }

        protected override void Dispose(bool disposing)
        {
            _dataService.Value.Dispose();
            base.Dispose(disposing);
        }

    }
}