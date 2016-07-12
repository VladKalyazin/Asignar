using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class DefectAddEditViewModel
    {
        public int DefectId { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public int PriorityId { get; set; }

        public int StatusId { get; set; }

        public string Description { get; set; }

    }
}
