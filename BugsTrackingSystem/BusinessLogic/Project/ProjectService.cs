using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsignarDBEntities;
using BugsTrackingSystem.Models;

namespace AsignarServices.Project
{
    public class ProjectService : IDisposable
    {
        private readonly AsignarDatabaseModel _databaseModel = new AsignarDatabaseModel();

        public ProjectsViewModel GetProjects()
        {
            var result = new ProjectsViewModel();

            foreach (var project in _databaseModel.Projects)
            {
                var projectView = new ProjectViewModel
                {
                    ProjectId = project.ProjectID,
                    Name = project.ProjectName,
                    Prefix = project.Prefix,
                    UsersCount = project.Users.Count
                };
                result.Add(projectView);
            }

            return result;
        }

        public void Dispose()
        {
            _databaseModel.Dispose();
        }
    }
}
