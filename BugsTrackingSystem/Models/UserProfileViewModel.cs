using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class UserProfileViewModel
    {
        public UserSimpleViewModel User { get; set; }

        public IEnumerable<DefectViewModel> UserDefects { get; set; }

        public PageInfo Paged { get; set; }
        
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}
