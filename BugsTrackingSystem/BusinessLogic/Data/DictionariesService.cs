using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsignarDBEntities;
using AsignarServices.AzureStorage;
using BugsTrackingSystem.Models;

namespace AsignarServices.Data
{
    public partial class AsignarDataService : IDisposable
    {
        public IEnumerable<DropdownItem> GetProjectNames()
        {
            return _databaseModel.Projects.Select((p) => new DropdownItem
            {
                Id = p.ProjectID,
                Name = p.ProjectName
            }).ToList();
        }

        public IEnumerable<DropdownItem> GetUserNames()
        {
            return _databaseModel.Users.Select((u) => new DropdownItem
            {
                Id = u.UserID,
                Name = u.FirstName + " " + u.Surname
            }).ToList();
        }

        public IEnumerable<DropdownItem> GetPrioritiesNames()
        {
            return _databaseModel.DefectPriorities.Select((dp) => new DropdownItem
            {
                Id = dp.DefectPriorityID,
                Name = dp.PriorityName
            }).ToList();
        }

        public IEnumerable<DropdownItem> GetStatusNames()
        {
            return _databaseModel.DefectStatuses.Select((s) => new DropdownItem
            {
                Id = s.DefectStatusID,
                Name = s.StatusName
            }).ToList();
        }

        public IEnumerable<DropdownItem> GetRoleNames()
        {
            return _databaseModel.Roles.Select((r) => new DropdownItem
            {
                Id = r.RoleID,
                Name = r.RoleName
            }).ToList();
        }
    }
}