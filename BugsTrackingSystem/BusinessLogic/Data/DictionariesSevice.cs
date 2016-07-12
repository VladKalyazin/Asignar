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
        public IEnumerable<string> GetProjectNames()
        {
            return _databaseModel.Projects.Select((p) => p.ProjectName).ToList();
        }

        public IEnumerable<string> GetUserNames()
        {
            return _databaseModel.Users.Select((u) => u.FirstName + " " + u.Surname).ToList();
        }

        public IEnumerable<string> GetPrioritiesNames()
        {
            return _databaseModel.DefectPriorities.Select((dp) => dp.PriorityName).ToList();
        }

        public IEnumerable<string> GetStatusNames()
        {
            return _databaseModel.DefectStatuses.Select((s) => s.StatusName).ToList();
        }
    }
}