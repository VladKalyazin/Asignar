using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsignarDBEntities;

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
    }
}
