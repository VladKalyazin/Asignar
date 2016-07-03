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
                CreationDate = DateTime.Now
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
                BlobStorageHelper blobHelper = new BlobStorageHelper();
                var priorityPhotos = blobHelper.GetDefectPriorityPhotos();
                var userPhotos = blobHelper.GetUserPhotos();

                var result = (from project in _databaseModel.Projects
                              where project.ProjectID == projectId
                              select new ProjectExtendedViewModel
                              {
                                  ProjectId = project.ProjectID,
                                  Name = project.ProjectName,
                                  Prefix = project.Prefix,
                                  UsersCount = project.Users.Count,
                                  DefectsCount = project.Defects.Count,
                                  Defects = (from defect in _databaseModel.Defects
                                             where defect.ProjectID == projectId
                                             select new DefectViewModel
                                             {
                                                 DefectId = defect.DefectID,
                                                 Subject = defect.Subject,
                                                 AssigneeUserName = defect.User.FirstName + " " + defect.User.Surname,
                                                 Status = defect.DefectStatus.StatusName,
                                                 PriorityId = defect.DefectPriorityID,
                                                 UserId = defect.AssigneeUserID
                                             }).ToList()
                              }).FirstOrDefault();

                foreach (var defect in result.Defects)
                {
                    defect.AssigneeUserPhoto = userPhotos[defect.UserId];
                    defect.PriorityPhoto = priorityPhotos[defect.PriorityId];
                }

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
