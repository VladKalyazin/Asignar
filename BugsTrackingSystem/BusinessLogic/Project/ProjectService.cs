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

            try
            {
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
            }
            catch
            {
                return null;
            }

            return result;
        }

        public void AddProject(ProjectViewModel projectModel)
        {
            var newProject = new AsignarDBEntities.Project
            {
                ProjectName = projectModel.Name,
                Prefix = projectModel.Prefix,
                CreationDate = DateTime.Now
            };

            _databaseModel.Projects.Add(newProject);
            _databaseModel.SaveChanges();
        }

        public void Dispose()
        {
            _databaseModel.Dispose();
        }
    }
}
