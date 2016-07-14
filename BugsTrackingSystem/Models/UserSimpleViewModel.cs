using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugsTrackingSystem.Models
{
    public class UserSimpleViewModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int DefectsCount { get; set; }

        public int ProjectsCount { get; set; }

        public string UserPhoto { get; set; }

        public string Role { get; set; }

        public int RoleId { get; set; }
    }
}
