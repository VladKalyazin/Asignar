using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsignarDBEntities;

namespace BusinessLogic.Account
{
    public class UserAction : IDisposable
    {
        private readonly AsignarDatabaseModel _databaseModel = new AsignarDatabaseModel();

        public bool ValidateUser(string email, string password)
        {
            bool isValid = false;

            var currentUser = (from user in _databaseModel.Users where user.Email == email && user.Password == password select user).FirstOrDefault();

            if (currentUser != null)
                isValid = true;

            return isValid;
        }

        public void Dispose()
        {
            _databaseModel.Dispose();
        }
    }
}
