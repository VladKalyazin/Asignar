using System.Collections.Generic;

namespace BugsTrackingSystem.Models
{
    public class AddNewUserToProjectViewModel
    {
        public IEnumerable<UserSimpleViewModel> Users { get; set; }
        public ProjectExtendedViewModel Project { get; set; }
    } 
}