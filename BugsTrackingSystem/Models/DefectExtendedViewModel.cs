using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BugsTrackingSystem.Models
{
    public class DefectExtendedViewModel
    {
        public int DefectId { get; set; }

        public string Subject { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Prefix { get; set; }

        public string Description { get; set; }

        public string AssigneeUserName { get; set; }

        public string AssigneeUserEmail { get; set; }

        public string AssigneeUserPhoto { get; set; }

        public int StatusId { get; set; }

        public string PriorityPhoto { get; set; }

        public string PriorityName { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public IEnumerable<SelectListItem> StatusesList { get; set; }
    }
}
