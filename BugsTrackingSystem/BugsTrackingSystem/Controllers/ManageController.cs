﻿using System;
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
using PagedList;

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

		public ActionResult Projects(int page = 1)
		{
            var projectService = new ProjectService();
		    var projects = projectService.GetProjects();

            int pageSize = 9;

            var projectsPerPages = projects.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = projects.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, projectViewModel = projectsPerPages };

            return View(ivm);
		}

        [HttpPost]
        public ActionResult RecieveForm()
        {
            var newProject = new ProjectViewModel
            {
                Name = Request.Form["Title"],
                Prefix = Request.Form["Key"]
            };

            var projectService = new ProjectService();
            projectService.AddProject(newProject);
            
            return RedirectToAction("Projects");
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