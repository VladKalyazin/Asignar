using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Web.Mvc;

namespace BugsTrackingSystem.Models
{
    public class NewDefectViewModel
    {
        public IEnumerable<SelectListItem> Projects { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }

        public IEnumerable<SelectListItem> Priority { get; set; }

        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
