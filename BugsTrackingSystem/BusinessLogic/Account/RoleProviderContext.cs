using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsignarDBEntities;

namespace BusinessLogic.Account
{
    public class RoleProviderContext : IDisposable
    {
        private readonly AsignarDatabaseModel _databaseModel = new AsignarDatabaseModel();

        public string GetRoleFromUserLogin(string login)
        {
            var currentUser = (from user in _databaseModel.Users
                               where user.Email == login
                               select user).FirstOrDefault();

            string userRole = currentUser?.Role?.RoleName;

            return userRole;
        }

        public bool IsUserInRole(string login, string roleName)
        {
            var currentUser = (from user in _databaseModel.Users
                               where user.Email == login
                               select user).FirstOrDefault();

            if (currentUser == null || currentUser.Role.RoleName != roleName)
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            _databaseModel.Dispose();
        }
    }
}
