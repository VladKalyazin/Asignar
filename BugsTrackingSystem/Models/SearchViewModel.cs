using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class SearchViewModel
    {
        public NewDefectViewModel Select { get; set; }

        public ProjectExtendedViewModel Project { get; set; }

        public string SelectedItem { get; set; }

        public IEnumerable<DefectViewModel> Defects { get; set; }

    }
}
