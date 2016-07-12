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
using System.Web.Security;
using AsignarServices.AzureStorage;
using AsignarServices.Data;
using BugsTrackingSystem.Models;
using BugsTrackingSystem.Filters;
using System.Web.Security;

namespace BugsTrackingSystem.Controllers
{
    [AsignarAuthenticate]
    [AsignarAuthorize]
    public class ManageController : Controller
    {
        private const int _projectsCountOnHomePage = 3;
        private const int _pageSize = 9;
        private const int _pageSizeHome = 8;

        private readonly Lazy<AsignarDataService> _dataService = new Lazy<AsignarDataService>(() => new AsignarDataService());

        public ManageController()
        {

        }

		public ActionResult UserProfile()
		{
			return View("Profile");
		}

		public ActionResult Home(int page = 1)
		{
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int id = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            int UserId = id;

            var defectsPerPages = _dataService.Value.GetUserSetOfDefects(UserId, _pageSizeHome, page - 1).ToList();
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = _pageSizeHome,
                TotalItems = _dataService.Value.GetCountUserDefects(UserId)
            };

            var model = new GetHomePageViewModel()
            {
                Projects = _dataService.Value.GetSetOfProjects(_projectsCountOnHomePage, 0).ToList(),
                Defects = defectsPerPages,
                Paged = pageInfo
            };
            return View(model);
        }

		public ActionResult Projects(int page = 1)
	    {
            var projectsPerPages = _dataService.Value.GetSetOfProjects(_pageSize, page - 1).ToList();
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = _pageSize,
                TotalItems = _dataService.Value.GetCountOfProjects()
            };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Projects = projectsPerPages };
            return View(ivm);
        }

        [HttpPost]
        public ActionResult RecieveForm()
        {
            var newProject = new ProjectViewModel
            {
                Name = Request.Form["Name"],
                Prefix = Request.Form["Key"]
            };

            _dataService.Value.AddProject(newProject);

            return RedirectToAction("Projects");
        }

        public ActionResult Users()
		{
            using (AsignarDataService _dataService = new AsignarDataService())
            {
                return View(_dataService.GetAllUsers().ToList());
            }
		}

        [HttpPost]
        public ActionResult AddUser(UserRegistrationViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                _dataService.Value.AddUser(newUser);
            }
            return RedirectToAction("Users");
        }

        public ActionResult Filters()
		{
			return View();
		}
        
        public ActionResult Project(int id, string sortOrder, bool direction = true)
        {
            int projId = id;
            
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Title";
            }
            
            DefectSortProperty sortSelected;
            sortSelected = (DefectSortProperty) Enum.Parse(typeof(DefectSortProperty), sortOrder, true);

            SortOrder orderSelected;

            if (direction)
            {
                orderSelected = (SortOrder)Enum.Parse(typeof(SortOrder), "Ascending", true);
            }
            else
            {
                orderSelected = (SortOrder)Enum.Parse(typeof(SortOrder), "Descending", true);
            }
            
            var model = new AddNewUserToProjectViewModel()
            {
               Users = _dataService.Value.GetUsersNotSignedToProject(projId),
               Project = _dataService.Value.GetFullProjectInfo(projId, sortSelected, orderSelected),
               SelectedItem = sortOrder,
               StraightSort = direction
            };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult SortProject()
        {
            int projId = Convert.ToInt32(Request.Form["Id"]);
            string selected = Request.Form["drop-down"];
            
            return RedirectToAction("Project", new {id = projId, sortOrder = selected});
        }


        public ActionResult Task()
		{
            var tableHelper = new TableStorageHelper();
            var comments = tableHelper.GetDefectComments(1);
			return View(comments);
		}

        protected override void Dispose(bool disposing)
        {
            _dataService.Value.Dispose();
            base.Dispose(disposing);
        }

    }
}