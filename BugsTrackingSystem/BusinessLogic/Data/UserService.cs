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
        public bool ValidateUser(string email, string password)
        {
            bool isValid = false;

            var currentUser = (from user in _databaseModel.Users where user.Email.ToUpper() == email.ToUpper() && user.Password == password select user).FirstOrDefault();

            if (currentUser != null)
                isValid = true;

            return isValid;
        }

        public IEnumerable<UserSimpleViewModel> GetAllUsers()
        {
            try
            {
                return from user in _databaseModel.Users
                       select new UserSimpleViewModel
                       {
                           UserId = user.UserID,
                           FirstName = user.FirstName,
                           Surname = user.Surname,
                           Email = user.Email,
                           DefectsCount = user.Defects.Count,
                           ProjectsCount = user.Projects.Count,
                           UserPhoto = user.PhotoLink
                       };
            }
            catch
            {
                //TODO exceptions
            }

            return null;
        }
    }
}
