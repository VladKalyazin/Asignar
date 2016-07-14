using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class FilterViewModel
    {
        public int FilterId { get; set; }

        public string Title { get; set; }

        public string Search { get; set; }

        public IEnumerable<int> ProjectIDs { get; set; }

        public IEnumerable<int> UserIDs { get; set; }

        public IEnumerable<int> StatusIDs { get; set; }

        public IEnumerable<int> PriorityIDs { get; set; }

    }
}
