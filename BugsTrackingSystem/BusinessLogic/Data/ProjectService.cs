using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsignarDBEntities;
using BugsTrackingSystem.Models;

namespace AsignarServices.Data
{
    public partial class AsignarDataService : IDisposable
    {
        public List<ProjectViewModel> GetAllProjects()
        {
            var result = (from project in _databaseModel.Projects
                          select new ProjectViewModel
                          {
                              ProjectId = project.ProjectID,
                              Name = project.ProjectName,
                              Prefix = project.Prefix,
                              UsersCount = project.Users.Count,
                              DefectsCount = project.Defects.Count
                          }).ToList();

            return result;
        }

        public int GetCountOfProjects() =>
            _databaseModel.Projects.Count();

        public List<ProjectViewModel> GetSetOfProjects(int countOfSet, int page)
        {
            var result = (from project in _databaseModel.Projects.OrderBy( (p) => p.CreationDate).Skip(page * countOfSet).Take(countOfSet)
                          select new ProjectViewModel
                          {
                              ProjectId = project.ProjectID,
                              Name = project.ProjectName,
                              Prefix = project.Prefix,
                              UsersCount = project.Users.Count,
                              DefectsCount = project.Defects.Count
                          }).ToList();

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

    }
}
