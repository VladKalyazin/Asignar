using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsignarDBEntities;
using AsignarServices.AzureStorage;
using BugsTrackingSystem.Models;
using System.Web.Mvc;

namespace AsignarServices.Data
{
    public partial class AsignarDataService : IDisposable
    {
        public IEnumerable<SelectListItem> GetProjectNames()
        {
            return _databaseModel.Projects.Select((p) => new SelectListItem
            {
                Value = p.ProjectID.ToString(),
                Text = p.ProjectName
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetUserNames()
        {
            return _databaseModel.Users.Select((u) => new SelectListItem
            {
                Value = u.UserID.ToString(),
                Text = u.FirstName + " " + u.Surname
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetPrioritiesNames()
        {
            return _databaseModel.DefectPriorities.Select((dp) => new SelectListItem
            {
                Value = dp.DefectPriorityID.ToString(),
                Text = dp.PriorityName
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetStatusNames()
        {
            return _databaseModel.DefectStatuses.Select((s) => new SelectListItem
            {
                Value = s.DefectStatusID.ToString(),
                Text = s.StatusName
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetRoleNames()
        {
            return _databaseModel.Roles.Select((r) => new SelectListItem
            {
                Value = r.RoleID.ToString(),
                Text = r.RoleName
            }).ToList();
        }
    }
}