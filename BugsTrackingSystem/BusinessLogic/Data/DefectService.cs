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
        public DefectViewModel GetDefect(int defectId)
        {
            return (from defect in _databaseModel.Defects
                    where defect.DefectID == defectId
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
                    }).SingleOrDefault();
        }

        public IEnumerable<DefectViewModel> GetUserDefects(int userId)
        {
            return (from defect in _databaseModel.Defects
                    where defect.AssigneeUserID == userId
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
                    }).ToList();
        }

    }
}