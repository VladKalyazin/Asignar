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
    public enum UserSortProperty
    {
        Name,
        Email,
        Projects,
        Defects
    }

    public partial class AsignarDataService : IDisposable
    {
        private const string _hashSalt = "wFZXS7z35VAh9NwfyBoM";

        public void UpdatePass()
        {
            foreach (var user in _databaseModel.Users)
            {
                user.Password = CalculateMD5HashWithSalt(user.Password);
            }
            _databaseModel.SaveChanges();
        }

        public int? ValidateUser(LoginViewModel user)
        {
            try
            {
                string hashPass = CalculateMD5HashWithSalt(user.Password);
                var currentUser = (from u in _databaseModel.Users
                                   where u.Email.ToUpper() == user.Email.ToUpper() && u.Password == hashPass
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

        public void DeleteUser(int userId)
        {
            try
            {
                var user = _databaseModel.Users.First(u => u.UserID == userId);
                if (user.Defects.Count > 0)
                {
                    throw new MethodAccessException($"This user assigns to {user.Defects.Count} tasks. Please release user from assignes before deleting.");
                }
                _databaseModel.Users.Remove(user);

                _databaseModel.SaveChanges();
            }
            catch (MethodAccessException e)
            {
                throw new Exception(e.Message);
            }
            catch
            {
                throw new Exception("Can not reassign the task.");
            }
        }

        public string GetRoleByUserId(int userId)
        {
            return _databaseModel.Users.Where((u) => u.UserID == userId).Select((user) => user.Role.RoleName).SingleOrDefault();
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
                    Password = CalculateMD5HashWithSalt(newUser.Password),
                    RoleID = Convert.ToInt32(newUser.RoleId),
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

        public int GetCountOfUsers() =>
            _databaseModel.Users.Count();

        public IEnumerable<UserSimpleViewModel> GetAllUsers(int countOfSet, int page)
        {
            try
            {
                return from user in _databaseModel.Users.OrderBy((p) => p.FirstName).Skip(page * countOfSet).Take(countOfSet)
                       select new UserSimpleViewModel
                       {
                           UserId = user.UserID,
                           FirstName = user.FirstName,
                           Surname = user.Surname,
                           Email = user.Email,
                           DefectsCount = user.Defects.Count,
                           ProjectsCount = user.Projects.Count,
                           UserPhoto = user.PhotoLink,
                           Role = user.Role.RoleName
                       };
            }
            catch
            {
                //TODO exceptions
            }

            return null;
        }

        public IEnumerable<UserSimpleViewModel> GetUsersNotSignedToProject(int projectId)
        {
            try
            {
                return from user in _databaseModel.Users
                       where !user.Projects.Any((u) => u.ProjectID == projectId)
                       select new UserSimpleViewModel
                       {
                           UserId = user.UserID,
                           FirstName = user.FirstName,
                           Surname = user.Surname,
                           Email = user.Email,
                           DefectsCount = user.Defects.Count,
                           ProjectsCount = user.Projects.Count,
                           UserPhoto = user.PhotoLink,
                           Role = user.Role.RoleName
                       };
            }
            catch
            {
                //TODO exceptions
            }

            return null;
        }

        public UserSimpleViewModel GetUserInfo(int userId)
        {
            try
            {
                return _databaseModel.Users.Where((u) => u.UserID == userId).
                        Select((u) => new UserSimpleViewModel
                        {
                            UserId = u.UserID,
                            FirstName = u.FirstName,
                            Surname = u.Surname,
                            Email = u.Email,
                            Role = u.Role.RoleName,
                            RoleId = u.Role.RoleID,
                            UserPhoto = u.PhotoLink,
                            ProjectsCount = u.Projects.Count,
                            DefectsCount = u.Defects.Count
                        }).FirstOrDefault();
            }
            catch
            {
                //TODO
            }

            return null;
        }

        public void EditUser(EditUserViewModel user)
        {
            try
            {
                var editEntity = _databaseModel.Users.First((u) => u.UserID == user.UserId);
                editEntity.FirstName = user.FirstName;
                editEntity.Surname = user.Surname;
                if (user.RoleId > 0)
                {
                    editEntity.RoleID = user.RoleId;
                }
                editEntity.Email = user.Email;

                _databaseModel.Users.Attach(editEntity);

                var entry = _databaseModel.Entry(editEntity);
                entry.Property((u) => u.FirstName).IsModified = true;
                entry.Property((u) => u.Surname).IsModified = true;
                if (user.RoleId > 0)
                {
                    entry.Property((u) => u.RoleID).IsModified = true;
                }
                entry.Property((u) => u.Email).IsModified = true;

                _databaseModel.SaveChanges();
            }
            catch
            {

            }
        }

        public void ChangeUserPassword(int userId, string newPassword)
        {
            try
            {
                _databaseModel.Users.First((u) => u.UserID == userId).Password = CalculateMD5HashWithSalt(newPassword);
                _databaseModel.SaveChanges();
            }
            catch
            {

            }
        }



        public static string CalculateMD5HashWithSalt(string input)
            => _CalculateMD5Hash(_CalculateMD5Hash(input) + _hashSalt);

        private static string _CalculateMD5Hash(string input)
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

        public void RefreshImageLink(int userId, string link)
        {
            try
            {
                _databaseModel.Users.Where((u) => u.UserID == userId).Single().PhotoLink = link;

                _databaseModel.SaveChanges();
            }
            catch
            {

            }
        }
    }
}
