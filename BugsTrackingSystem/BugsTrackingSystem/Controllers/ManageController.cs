using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AsignarServices.AzureStorage;
using AsignarServices.Project;
using BugsTrackingSystem.Models;

namespace BugsTrackingSystem.Controllers
{
    [AllowAnonymous]
    public class ManageController : Controller
    {
        public ManageController()
        {

        }

		public ActionResult UserProfile()
		{
			return View("Profile");
		}

		public ActionResult Home()
		{
            var projectService = new ProjectService();

            return View(projectService.GetProjects());
        }

		public ActionResult Projects()
		{
            var projectService = new ProjectService();
             
			return View(projectService.GetProjects());
		}

		public ActionResult Users()
		{
			return View(BlobStorageHelper.GetPhotoUrls());
		}

		public ActionResult Filters()
		{
			return View();
		}

		public ActionResult Project()
		{
			return View();
		}
	
		public ActionResult Task()
		{
			return View();
		}
	}
}