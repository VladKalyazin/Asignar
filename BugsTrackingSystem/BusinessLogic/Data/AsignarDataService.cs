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
        private readonly AsignarDatabaseModel _databaseModel = new AsignarDatabaseModel();

        public void Dispose()
        {
            _databaseModel.Dispose();
        }
 
    }
}
