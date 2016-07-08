using System.Collections.Generic;

namespace BugsTrackingSystem.Models
{
    public class GetHomePageViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public IEnumerable<DefectViewModel> Defects { get; set; }

        public PageInfo Paged { get; set; }
    }
}