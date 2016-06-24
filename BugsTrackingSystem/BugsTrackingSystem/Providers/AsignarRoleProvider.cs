using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Helpers;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;
using BusinessLogic.Account;

namespace BugsTrackingSystem.Providers
{
    class AsignarRoleProvider : RoleProvider
    {
        private readonly RoleProviderContext _providerContext = new RoleProviderContext();

        public override string[] GetRolesForUser(string login)
        {
            string role = _providerContext.GetRoleFromUserLogin(login);

            if (role == null)
            {
                return new string[] { };
            }
            else
            {
                return new string[] { role };
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return _providerContext.IsUserInRole(username, roleName);
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }

}
