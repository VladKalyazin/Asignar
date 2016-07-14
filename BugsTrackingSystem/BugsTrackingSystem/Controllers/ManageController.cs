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
        private const int _pageSizeHome = 8;

        private readonly Lazy<AsignarDataService> _dataService = new Lazy<AsignarDataService>(() => new AsignarDataService());

        public ManageController()
        {

        }

        [ChildActionOnly]
        public ActionResult ProfilePartial()
        {
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int id = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            UserSimpleViewModel currentUser = _dataService.Value.GetUserInfo(id);

            return PartialView(currentUser);
        }

        public ActionResult Profile(int page = 1)
		{
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int id = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            var defectsPerPages = _dataService.Value.GetUserSetOfDefects(id, _pageSizeHome, page - 1).ToList();
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = _pageSizeHome,
                TotalItems = _dataService.Value.GetCountUserDefects(id)
            };

            var userProjects = _dataService.Value.GetUserProjects(id);

            var model = new UserProfileViewModel()
		    {
                UserDefects = defectsPerPages,
                Paged = pageInfo,
                User = _dataService.Value.GetUserInfo(id),
                Projects = userProjects.Take(4),
                Roles = _dataService.Value.GetRoleNames()
            };
            
            return View(model);
		}

        [HttpPost]
        public ActionResult EditUserViewModel()
        {
            int Id = Convert.ToInt32(Request.Form["Id"]);
            string role = Request.Form["Role"];

            var newUser = new EditUserViewModel
            {
                UserId = Id,
                FirstName = Request.Form["Name"],
                Surname = Request.Form["Surname"],
                Email = Request.Form["Email"],
                RoleId = Convert.ToInt32(role)
            };

            _dataService.Value.EditUser(newUser);

            return RedirectToAction("Profile", new { id = Id });
        }

        [HttpPost]
        public ActionResult ChangePassword()
        {
            int Id = Convert.ToInt32(Request.Form["Id"]);
            string Password = Request.Form["Password"];
            string RepeatPassword = Request.Form["RepeatPassword"];

            if (Password != RepeatPassword)
            {
                return RedirectToAction("Profile", new { id = Id });
            }

            _dataService.Value.ChangeUserPassword(Id, Password);

            return RedirectToAction("Profile", new { id = Id });
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

            var model = new UserProfileHomeViewModel()
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

            return RedirectToAction("Project", new {id = newProject.ProjectId});
        }

        [ChildActionOnly]
        public ActionResult CreateNewTaskView()
        {
            var addDefect = new NewDefectViewModel
            {
                Projects = _dataService.Value.GetProjectNames(),
                Users = _dataService.Value.GetUserNames(),
                Priority = _dataService.Value.GetPrioritiesNames(),
                Status = _dataService.Value.GetStatusNames()
            };
            return PartialView(addDefect);
        }

        [HttpPost]
        public ActionResult AddNewTask()
        {
            var newTask = new DefectAddEditViewModel
            {
                Name = Request.Form["Name"],
                ProjectId = Convert.ToInt32(Request.Form["Project"]),
                PriorityId = Convert.ToInt32(Request.Form["Priority"]),
                StatusId = Convert.ToInt32(Request.Form["Status"]),
                UserId = Convert.ToInt32(Request.Form["Owner"]),
                Description = Request.Form["Description"]
            };

            _dataService.Value.AddDefect(newTask);

            return RedirectToAction("Task", new { id = newTask.DefectId});
        }

        [HttpPost]
        public ActionResult EditTask()
        {
            return RedirectToAction("Task");
        }

        public ActionResult Users(int page = 1)
		{
            var usersPerPages = _dataService.Value.GetAllUsers(_pageSize, page - 1).ToList();
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = _pageSize,
                TotalItems = _dataService.Value.GetCountOfUsers()
            };
            IndexViewModel ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                Users = usersPerPages,
                Roles = _dataService.Value.GetRoleNames()
            };
            return View(ivm);
		}

        [HttpPost]
        public ActionResult AddUser(UserRegistrationViewModel newUser)
        {
            newUser.RoleId = Request.Form["Role"];
            _dataService.Value.AddUser(newUser);
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

            ViewBag.sortOrder = sortOrder;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult SortProject()
        {
            int projId = Convert.ToInt32(Request.Form["Id"]);
            string selected = Request.Form["drop-down"];
            
            return RedirectToAction("Project", new {id = projId, sortOrder = selected});
        }

        [HttpPost]
        public ActionResult EditProjectName()
        {
            int Id = Convert.ToInt32(Request.Form["Id"]);
            string newName = Request.Form["NewName"];

            _dataService.Value.EditProject(Id, newName);

            return RedirectToAction("Project", new { id = Id });
        }

        public ActionResult Task(int id)
        {
            var changeDefect = new NewDefectViewModel
            {
                Projects = _dataService.Value.GetProjectNames(),
                Users = _dataService.Value.GetUserNames(),
                Priority = _dataService.Value.GetPrioritiesNames(),
                Status = _dataService.Value.GetStatusNames()
            };

            var defect = new TaskViewModel
            {
                DefectInfo = _dataService.Value.GetDefectExtendedInfo(id),
                ChangeDefect = changeDefect
            };
            
            return View(defect);
		}

        public ActionResult Search()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _dataService.Value.Dispose();
            base.Dispose(disposing);
        }

    }
}