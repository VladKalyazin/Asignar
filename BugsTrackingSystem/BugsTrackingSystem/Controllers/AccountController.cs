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
using BusinessLogic.Account;

namespace BugsTrackingSystem.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserHelper _userHelper = new UserHelper();

        public AccountController()
        {

        }

        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        //
        // GET: /Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_userHelper.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    return RedirectToAction("Home", "Manage");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }

            return View(model);
        }

    }
}