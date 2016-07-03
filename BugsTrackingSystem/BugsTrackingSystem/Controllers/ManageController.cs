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
using AsignarServices.Data;
using BugsTrackingSystem.Models;

namespace BugsTrackingSystem.Controllers
{
    [AllowAnonymous]
    public class ManageController : Controller
    {
        private const int _pageSize = 9;

        public ManageController()
        {

        }

		public ActionResult UserProfile()
		{
			return View("Profile");
		}

		public ActionResult Home()
		{
            using (AsignarDataService _dataService = new AsignarDataService())
            {
                return View(_dataService.GetAllProjects());
            }
        }

		public ActionResult Projects(int page = 1)
		{
            using (AsignarDataService _dataService = new AsignarDataService())
            {
                var projectsPerPages = _dataService.GetSetOfProjects(_pageSize, page - 1);
                PageInfo pageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = _pageSize,
                    TotalItems = _dataService.GetCountOfProjects()
                };
                IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, projectViewModel = projectsPerPages };
                return View(ivm);
            }
        }

        [HttpPost]
        public ActionResult RecieveForm()
        {
            var newProject = new ProjectViewModel
            {
                Name = Request.Form["Title"],
                Prefix = Request.Form["Key"]
            };

            using (AsignarDataService _dataService = new AsignarDataService())
            {
                _dataService.AddProject(newProject);
            }

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