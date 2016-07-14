using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class FilterSimpleViewModel
    {
        public int FilterId { get; set; }

        public string Title { get; set; }

        public string Search { get; set; }

        public IEnumerable<string> Projects { get; set; }

        public IEnumerable<string> Users { get; set; }

        public IEnumerable<string> Statuses { get; set; }

        public IEnumerable<string> Priorities { get; set; }

    }
}
