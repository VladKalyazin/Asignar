using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class DefectViewModel
    {
        public int DefectId { get; set; }

        public string Prefix { get; set; }

        public string Subject { get; set; }

        public string AssigneeUserName { get; set; }

        public string AssigneeUserPhoto { get; set; }

        public string Status { get; set; }

        public string PriorityPhoto { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
