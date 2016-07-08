using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsignarDBEntities;
using BugsTrackingSystem.Models;
using AsignarServices.AzureStorage;
using System.Security.Cryptography;

namespace AsignarServices.Data
{
    public partial class AsignarDataService : IDisposable
    {
        private const string _hashSalt = "wFZXS7z35VAh9NwfyBoM";

        public int? ValidateUser(LoginViewModel user)
        {
            try
            {
                var currentUser = (from u in _databaseModel.Users
                                   where u.Email.ToUpper() == user.Email.ToUpper() && u.Password == CalculateMD5HashWithSalt(user.Password)
                                   select u).SingleOrDefault();

                if (currentUser != null)
                    return currentUser.UserID;

                return null;
            }
            catch
            {
                // TODO
            }

            return null;
        }

        public void AddUser(UserRegistrationViewModel newUser)
        {
            try
            {
                _databaseModel.Users.Add(new User
                {
                    FirstName = newUser.FirstName,
                    Surname = newUser.Surname,
                    Email = newUser.Email,
                    Password = CalculateMD5Hash(newUser.Password),
                    RoleID = _databaseModel.Roles.Where((r) => r.RoleName == "common").Select((role) => role.RoleID).Single(),
                    RegistrationDate = DateTime.UtcNow
                });

                _databaseModel.SaveChanges();

                var queueService = new QueueStorageHelper();
                queueService.SendNewUserMessage(new UserQueueModel
                {
                    FirstName = newUser.FirstName,
                    Surname = newUser.Surname,
                    Email = newUser.Email
                });
            }
            catch
            {
                //TODO exceptions
            }
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

        public static string CalculateMD5HashWithSalt(string input)
            => CalculateMD5Hash(CalculateMD5Hash(input) + _hashSalt);

        private static string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}
