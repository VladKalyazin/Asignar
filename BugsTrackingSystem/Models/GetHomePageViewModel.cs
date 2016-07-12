using System.Collections.Generic;

namespace BugsTrackingSystem.Models
{
    public class UserProfileHomeViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public IEnumerable<DefectViewModel> Defects { get; set; }

        public PageInfo Paged { get; set; }
    }
}