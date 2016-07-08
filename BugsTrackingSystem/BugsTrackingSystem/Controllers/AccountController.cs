﻿using System;
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

namespace BugsTrackingSystem.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AsignarDataService _dataService = new AsignarDataService();

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
        // GET: /Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        public ActionResult ResetPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            Lazy<int?> userId = new Lazy<int?>( () => _dataService.ValidateUser(model) );

            if (ModelState.IsValid)
            {
                if (userId.Value != null)
                {
                    return RedirectToAction("Home", "Manage");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }

            return View(model);
        }

        public new void Dispose()
        {
            base.Dispose();
            _dataService.Dispose();
        }

    }
}