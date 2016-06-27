using System;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BugsTrackingSystem.Providers;

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
			return View();
		}

		public ActionResult Projects()
		{
			return View();
		}

		public ActionResult Users()
		{
			return View();
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