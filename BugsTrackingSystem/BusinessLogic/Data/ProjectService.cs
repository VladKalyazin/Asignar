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
        public IEnumerable<ProjectViewModel> GetAllProjects()
        {
            try
            {
                return from project in _databaseModel.Projects
                        select new ProjectViewModel
                        {
                            ProjectId = project.ProjectID,
                            Name = project.ProjectName,
                            Prefix = project.Prefix,
                            UsersCount = project.Users.Count,
                            DefectsCount = project.Defects.Count
                        };
            }
            catch
            {
                //TODO exceptions
            }

            return null;
        }

        public IEnumerable<ProjectViewModel> GetUserProjects(int userId)
        {
            try
            {
                return from project in _databaseModel.Projects
                       where project.Users.Any((u) => u.UserID == userId)
                       select new ProjectViewModel
                       {
                           ProjectId = project.ProjectID,
                           Name = project.ProjectName,
                           Prefix = project.Prefix,
                           UsersCount = project.Users.Count,
                           DefectsCount = project.Defects.Count
                       };
            }
            catch
            {
                //TODO exceptions
            }

            return null;
        }

        public int GetCountOfProjects() =>
            _databaseModel.Projects.Count();

        public IEnumerable<ProjectViewModel> GetSetOfProjects(int countOfSet, int page)
        {
            try
            {
                return from project in _databaseModel.Projects.OrderBy((p) => p.CreationDate).Skip(page * countOfSet).Take(countOfSet)
                        select new ProjectViewModel
                        {
                            ProjectId = project.ProjectID,
                            Name = project.ProjectName,
                            Prefix = project.Prefix,
                            UsersCount = project.Users.Count,
                            DefectsCount = project.Defects.Count
                        };
            }
            catch
            {
                //TODO exceptions
            }

            return null;
        }

        public void AddProject(ProjectViewModel projectModel)
        {
            var newProject = new AsignarDBEntities.Project
            {
                ProjectName = projectModel.Name,
                Prefix = projectModel.Prefix,
            };

            try
            {
                _databaseModel.Projects.Add(newProject);
                _databaseModel.SaveChanges();
            }
            catch
            {
                //TODO exceptions
            }
        }

        public ProjectExtendedViewModel GetFullProjectInfo(int projectId)
        {
            try
            {
                var result = (from project in _databaseModel.Projects
                              where project.ProjectID == projectId
                              select new ProjectExtendedViewModel
                              {
                                  ProjectId = project.ProjectID,
                                  Name = project.ProjectName,
                                  Prefix = project.Prefix,
                                  UsersCount = project.Users.Count,
                                  DefectsCount = project.Defects.Count,
                                  Defects = from defect in _databaseModel.Defects
                                            where defect.ProjectID == projectId
                                            select new DefectViewModel
                                            {
                                                DefectId = defect.DefectID,
                                                Subject = defect.Subject,
                                                AssigneeUserName = defect.User.FirstName + " " + defect.User.Surname,
                                                Status = defect.DefectStatus.StatusName,
                                                PriorityPhoto = defect.DefectPriority.PhotoLink,
                                                AssigneeUserPhoto = defect.User.PhotoLink,
                                                CreationDate = defect.CreationDate,
                                                ModificationDate = defect.ModificationDate
                                            },
                                  Users = from user in project.Users
                                          select new UserSimpleViewModel
                                          {
                                              UserId = user.UserID,
                                              FirstName = user.FirstName,
                                              Surname = user.Surname,
                                              Email = user.Email,
                                              DefectsCount = user.Defects.Count,
                                              ProjectsCount = user.Projects.Count,
                                              UserPhoto = user.PhotoLink
                                          }
                              }).FirstOrDefault();

                return result;
            }
            catch
            {
                //TODO exceptions
            }

            return null;
        }

    }
}
