using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;
using System.Web.UI.DataVisualization.Charting;
using AsignarServices.AzureStorage;
using AsignarServices.Data;
using BugsTrackingSystem.Models;
using BugsTrackingSystem.Filters;
using System.IO;
using ImageResizer;

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

        public ActionResult Profile(int userId, int page = 1)
		{
            /*var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int id = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);*/

            var defectsPerPages = _dataService.Value.GetUserSetOfDefects(userId, _pageSizeHome, page - 1).ToList();
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = _pageSizeHome,
                TotalItems = _dataService.Value.GetCountUserDefects(userId)
            };

            var userProjects = _dataService.Value.GetUserProjects(userId, 4, 0);

            var model = new UserProfileViewModel()
		    {
                UserDefects = defectsPerPages,
                Paged = pageInfo,
                User = _dataService.Value.GetUserInfo(userId),
                Projects = userProjects,
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
            };

            newUser.RoleId = !string.IsNullOrEmpty(role) ? Convert.ToInt32(role) : 0;

            _dataService.Value.EditUser(newUser);

            return RedirectToAction("Profile", new { userId = Id });
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            int Id = Convert.ToInt32(Request.Form["userId"]);

            /*if (file != null)
            {
                //Declare a new dictionary to store the parameters for the image versions.
                var versions = new Dictionary<string, string>();

                var path = Server.MapPath("~/Images/");

                //Define the versions to generate
                versions.Add("_small", "maxwidth=600&maxheight=600&format=jpg");
                versions.Add("_medium", "maxwidth=900&maxheight=900&format=jpg");
                versions.Add("_large", "maxwidth=1200&maxheight=1200&format=jpg");

                //Generate each version
                foreach (var suffix in versions.Keys)
                {
                    file.InputStream.Seek(0, SeekOrigin.Begin);

                    //Let the image builder add the correct extension based on the output file type
                    ImageBuilder.Current.Build(
                        new ImageJob(
                            file.InputStream,
                            path + file.FileName + suffix,
                            new Instructions(versions[suffix]),
                            false,
                            true));
                }
                //_dataService.Value.UpdatePhoto(Id, file);

                return RedirectToAction("Profile", new { userId = Id });
            }*/

            if (file != null)
            {
                WebImage img = new WebImage(file.InputStream);

                var width = img.Width;
                var height = img.Height;

                if (width > height)
                {
                    var leftRightCrop = (width - height) / 2;
                    img.Crop(0, leftRightCrop, 0, leftRightCrop);
                }
                else if (height > width)
                {
                    var topBottomCrop = (height - width) / 2;
                    img.Crop(topBottomCrop, 0, topBottomCrop, 0);
                }

                var byteImage = img.GetBytes();
                var blobHelper = new BlobStorageHelper();
                blobHelper.UploadPhoto(Id, byteImage);

                string url = blobHelper.GetUserPhotoUrl(Id);
                _dataService.Value.RefreshImageLink(Id, url);
            }

            return RedirectToAction("Profile", new { userId = Id });
        }
        
        public ActionResult ClearPhoto(int Id)
        {
            var blobHelper = new BlobStorageHelper();
            blobHelper.DeletePhoto(Id);

            _dataService.Value.RefreshImageLink(Id, null);

            return RedirectToAction("Profile", new { userId = Id });
        }

        [HttpPost]
        public ActionResult ChangePassword()
        {
            int Id = Convert.ToInt32(Request.Form["Id"]);
            string Password = Request.Form["Password"];
            string RepeatPassword = Request.Form["RepeatPassword"];

            if (Password != RepeatPassword)
            {
                return RedirectToAction("Profile", new { userId = Id });
            }

            _dataService.Value.ChangeUserPassword(Id, Password);

            return RedirectToAction("Profile", new { userId = Id });
        }

        public ActionResult Home(int page = 1)
		{
            var helper = new BlobStorageHelper();
            var list = (helper.GetAttachmentsUrls(2));

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

            var userProjects = _dataService.Value.GetUserProjects(id, 4, 0);

            var model = new UserProfileHomeViewModel()
            {
                Projects = userProjects,
                Defects = defectsPerPages,
                Paged = pageInfo
            };
            return View(model);
        }

		public ActionResult Projects(int page = 1)
	    {
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int id = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            IEnumerable<ProjectViewModel> userProjects;

            if (User.IsInRole("Admin"))
		    {
                userProjects = _dataService.Value.GetSetOfProjects(_pageSize, page - 1).ToList();
		    }
		    else
		    {
                userProjects = _dataService.Value.GetUserProjects(id, _pageSize, page - 1);
            }

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = _pageSize,
                TotalItems = User.IsInRole("Admin") ? _dataService.Value.GetCountOfProjects() : _dataService.Value.GetUserProjectsCount(id)
            };
            IndexViewModel ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                Projects = userProjects
            };
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

        public ActionResult DeleteProject(int projectId, int selectedPage = 1)
        {
            _dataService.Value.DeleteProject(projectId);
            return RedirectToAction("Projects", new { page = selectedPage } );
        }

        [ChildActionOnly]
        public ActionResult CreateNewTaskView()
        {
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int userId = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            var addDefect = new NewDefectViewModel
            {
                Projects = _dataService.Value.GetProjectNames(userId),
                Users = _dataService.Value.GetUserNames(),
                Priority = _dataService.Value.GetPrioritiesNames(),
                Status = _dataService.Value.GetStatusNames()
            };
            return PartialView(addDefect);
        }

        public ActionResult GetUsersFromProject(int projectId, int selectedUserId)
        {
            var Users = _dataService.Value.GetUserNames(projectId);
            Users.First(u => u.Value == selectedUserId.ToString()).Selected = true;

            return Json(Users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProjectsFromUser(int userId, int selectedProjectId)
        {
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int loginId = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            var Projects = _dataService.Value.GetProjectNames(userId, loginId);
            Projects.First(p => p.Value == selectedProjectId.ToString()).Selected = true;

            return Json(Projects, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddNewTask(IEnumerable<HttpPostedFileBase> files)
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

            var blobHelper = new BlobStorageHelper();

            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    byte[] data;
                    using (Stream inputStream = file.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        data = memoryStream.ToArray();
                    }

                    blobHelper.UploadAttachment(newTask.DefectId, data, file.FileName);
                    string link = blobHelper.GetAttachmentUrl(newTask.DefectId, file.FileName);
                    _dataService.Value.AddAttachment(newTask.DefectId, file.FileName, link);
                }
            }

            return RedirectToAction("Task", new { id = newTask.DefectId});
        }

        [HttpPost]
        public ActionResult EditTask()
        {
            var defect = new DefectAddEditViewModel
            {
                DefectId = Convert.ToInt32(Request.Form["DefectId"]),
                Name = Request.Form["Name"],
                ProjectId = Convert.ToInt32(Request.Form["Project"]),
                UserId = Convert.ToInt32(Request.Form["Owner"]),
                PriorityId = Convert.ToInt32(Request.Form["Priority"]),
                StatusId = Convert.ToInt32(Request.Form["Status"]),
                Description = Request.Form["Description"]
            };

            _dataService.Value.EditDefect(defect);
            return RedirectToAction("Task", new {id = defect.DefectId});
        }

        public ActionResult DeleteDefect(int defId, int projId)
        {
            _dataService.Value.DeleteDefect(defId);
            return RedirectToAction("Project", new {id = projId});
        }

        public ActionResult Users(int page = 1)
		{
            if (!User.IsInRole("Admin"))
                return View("Error");

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

        public ActionResult Filters(int page = 1)
		{
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int id = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            var multipleChoice = new NewDefectViewModel
            {
                Projects = _dataService.Value.GetProjectNames(),
                Users = _dataService.Value.GetUserNames(),
                Priority = _dataService.Value.GetPrioritiesNames(),
                Status = _dataService.Value.GetStatusNames()
            };

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = _pageSize,
                TotalItems = _dataService.Value.GetCountOfUsers()
            };

            var filter = new FiltersPageViewModel
            {
                Select = multipleChoice,
                FilterInfo = _dataService.Value.GetFilters(id, _pageSize, page - 1).ToList(),
                PageInfo = pageInfo
            };

            return View(filter);
		}
        
        public ActionResult DeleteUser(int userId)
        {
            _dataService.Value.DeleteUser(userId);
            return RedirectToAction("Users");
        }

        [HttpPost]
        public ActionResult AddNewFilter()
        {
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int id = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            string s = Request.Form["Priorities"];
            IEnumerable<int> priority = !string.IsNullOrEmpty(s) ? Array.ConvertAll(s.Split(','), int.Parse) : Enumerable.Empty<int>();

            string r = Request.Form["Projects"];
            IEnumerable<int> projects = !string.IsNullOrEmpty(r) ? Array.ConvertAll(r.Split(','), int.Parse) : Enumerable.Empty<int>();

            string q = Request.Form["Statuses"];
            IEnumerable<int> statuses = !string.IsNullOrEmpty(q) ? Array.ConvertAll(q.Split(','), int.Parse) : Enumerable.Empty<int>();

            string t = Request.Form["Assignees"];
            IEnumerable<int> users = !string.IsNullOrEmpty(t) ? Array.ConvertAll(t.Split(','), int.Parse) : Enumerable.Empty<int>();

            var filter = new FilterViewModel
            {
                Title = Request.Form["Name"],
                Search = Request.Form["Search"],
                PriorityIDs = priority,
                StatusIDs = statuses,
                ProjectIDs = projects,
                UserIDs = users
            };

            _dataService.Value.AddFilter(id, filter);

            return RedirectToAction("Filters");
        }

        [HttpPost]
        public ActionResult AddNewFilterFromSearch(string title, string search, string [] priorities, string[] statuses, string[] projects, string[] assignees)
        {
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int id = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            IEnumerable<int> myPriorities = !(priorities == null) ? priorities.Select(int.Parse).ToArray() : Enumerable.Empty<int>();
            IEnumerable <int> myStatuses = !(statuses == null) ? statuses.Select(int.Parse).ToArray() : Enumerable.Empty<int>();
            IEnumerable<int> myProjects = !(projects == null) ? projects.Select(int.Parse).ToArray() : Enumerable.Empty<int>();
            IEnumerable<int> myAssignees = !(assignees == null) ? assignees.Select(int.Parse).ToArray() : Enumerable.Empty<int>();
            
            var filter = new FilterViewModel
            {
                Title = title,
                Search = search,
                PriorityIDs = myPriorities,
                StatusIDs = myStatuses,
                ProjectIDs = myProjects,
                UserIDs = myAssignees
            };

            _dataService.Value.AddFilter(id, filter);

            return RedirectToAction("Filters");
        }

        public ActionResult DeleteFilter(int filterId)
        {
            _dataService.Value.DeleteFilter(filterId);
            return RedirectToAction("Filters");
        }

        public ActionResult OpenFilter(int filterId)
        {
            FilterViewModel openfilter = _dataService.Value.GetFilter(filterId);

            return RedirectToAction("Search", "Manage", new {exsitingFilter = openfilter});
        }

        public ActionResult Project(int id, string sortOrder, bool direction = true)
        {
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int _userId = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);
            if (!User.IsInRole("Admin") && !_dataService.Value.IsUserInProject(_userId, id))
            {
                return View("Error");
            }

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
        
        public ActionResult DeleteUserFromProject(int projId, int userId)
        {
            _dataService.Value.RemoveUserFromProject(userId, projId);
            return RedirectToAction("Project", new { id = projId });
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

        public ActionResult Search(string sortOrder = "Title", int page = 1, int? filterId = null)
        {
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int id = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Title";
            }

            DefectSortProperty sortSelected;
            sortSelected = (DefectSortProperty)Enum.Parse(typeof(DefectSortProperty), sortOrder, true);
            
            var changeDefect = new NewDefectViewModel
            {
                Projects = _dataService.Value.GetProjectNames(),
                Users = _dataService.Value.GetUserNames(),
                Priority = _dataService.Value.GetPrioritiesNames(),
                Status = _dataService.Value.GetStatusNames()
            };

            FilterViewModel filter;

            if (filterId == null)
            {
                IEnumerable<int> priority;
                IEnumerable<int> projects;
                IEnumerable<int> statuses;
                IEnumerable<int> users;

                string priorityStr = Request.Form["Priorities"];
                priority = !string.IsNullOrEmpty(priorityStr) ? Array.ConvertAll(priorityStr.Split(','), int.Parse) : Enumerable.Empty<int>();

                string projectsStr = Request.Form["Projects"];
                projects = !string.IsNullOrEmpty(projectsStr) ? Array.ConvertAll(projectsStr.Split(','), int.Parse) : Enumerable.Empty<int>();

                string statusStr = Request.Form["Statuses"];
                statuses = !string.IsNullOrEmpty(statusStr) ? Array.ConvertAll(statusStr.Split(','), int.Parse) : Enumerable.Empty<int>();

                string usersStr = Request.Form["Assignees"];
                users = !string.IsNullOrEmpty(usersStr) ? Array.ConvertAll(usersStr.Split(','), int.Parse) : Enumerable.Empty<int>();

                filter = new FilterViewModel
                {
                    Search = Request.Form["Search"],
                    PriorityIDs = priority,
                    StatusIDs = statuses,
                    ProjectIDs = projects,
                    UserIDs = users
                };
            }
            else
            {
                filter = _dataService.Value.GetFilter(filterId.Value);
            }


            var defects = _dataService.Value.FindDefects(filter, _pageSizeHome, page - 1, sortSelected);

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = _pageSize,
                TotalItems = _dataService.Value.CountOfDefects(filter)
            };

            var model = new SearchViewModel
            {
                Select = changeDefect,
                SelectedItem = sortOrder,
                Defects = defects,
                Filter = filter,
                PageInfo = pageInfo,
                FilterInfo = _dataService.Value.GetAllFilters(id).ToList()
            };

            return View(model);
        }

        //[HttpPost]
        //public ActionResult SaveFilterView()
        //{
        //    string s = Request.Form["Priorities"];
        //    IEnumerable<int> priority = !string.IsNullOrEmpty(s) ? Array.ConvertAll(s.Split(','), int.Parse) : Enumerable.Empty<int>();

        //    string r = Request.Form["Projects"];
        //    IEnumerable<int> projects = !string.IsNullOrEmpty(r) ? Array.ConvertAll(r.Split(','), int.Parse) : Enumerable.Empty<int>();

        //    string q = Request.Form["Statuses"];
        //    IEnumerable<int> statuses = !string.IsNullOrEmpty(q) ? Array.ConvertAll(q.Split(','), int.Parse) : Enumerable.Empty<int>();

        //    string t = Request.Form["Assignees"];
        //    IEnumerable<int> users = !string.IsNullOrEmpty(t) ? Array.ConvertAll(t.Split(','), int.Parse) : Enumerable.Empty<int>();

        //    var filter = new FilterViewModel
        //    {
        //        PriorityIDs = priority,
        //        StatusIDs = statuses,
        //        ProjectIDs = projects,
        //        UserIDs = users
        //    };
        //    HttpContext.Response.StatusCode = 200;
        //    return PartialView(filter);
        //}

        [HttpPost]
        public ActionResult AddComment()
        {
            int defectId = Convert.ToInt32(Request.Form["defectId"]);
            string text = Request.Form["text"];
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int userId = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);

            var commentHelper = new TableStorageHelper();
            commentHelper.InsertComment(defectId, userId, text);

            return RedirectToAction("Task", new { id = defectId });
        }

        [HttpPost]
        public ActionResult AddUsersToProject()
        {
            int projectId = Convert.ToInt32(Request.Form["Id"]);

            string s = Request.Form["userIds"];
            s = s.Replace("false,", "");
            s = s.Replace("false", "");
            s = s.Trim(',');
            IEnumerable<int> userIds = !string.IsNullOrEmpty(s) ? Array.ConvertAll(s.Split(','), int.Parse) : Enumerable.Empty<int>();
            _dataService.Value.AddUsersToProject(projectId, userIds);

            return RedirectToAction("Project", new { id = projectId });
        }

        public ActionResult AssignDefectToUser(int defectId)
        {
            var authCookie = Request.Cookies["Auth"];
            var enc = authCookie.Value;
            int userId = Convert.ToInt32(FormsAuthentication.Decrypt(enc).Name);
            _dataService.Value.AssignDefect(defectId, userId);

            return RedirectToAction("Task", new { id = defectId });
        }

        protected override void Dispose(bool disposing)
        {
            _dataService.Value.Dispose();
            base.Dispose(disposing);
        }

    }
}