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
using System.Web.Security;
using AsignarServices.AzureStorage;
using AsignarServices.Data;
using BugsTrackingSystem.Models;
using BugsTrackingSystem.Filters;

namespace BugsTrackingSystem.Controllers
{
    [AsignarAuthenticate]
    [AsignarAuthorize]
    public class ManageController : Controller
    {
        private const int _projectsCountOnHomePage = 3;
        private const int _pageSize = 9;

        private readonly Lazy<AsignarDataService> _dataService = new Lazy<AsignarDataService>(() => new AsignarDataService());

        public ManageController()
        {

        }

		public ActionResult UserProfile()
		{
			return View("Profile");
		}

		public ActionResult Home()
		{
            return View(_dataService.Value.GetSetOfProjects(_projectsCountOnHomePage, 0).ToList());
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
        
		public ActionResult Project(int id)
        {
            int projId = id;
            return View(_dataService.Value.GetFullProjectInfo(projId));
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