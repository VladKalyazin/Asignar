using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Transactions;
using System.Globalization;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using BugsTrackingSystem.Models;
using AsignarDBEntities;

namespace AsignarServices.AzureStorage
{
    public class CommentEntity : TableEntity
    {
        public const string RowKeyFormat = "yyyyMMddHHmmssffff";

        public CommentEntity(DateTime creationDate, string commentText, int userId, int defectId)
        {
            this.PartitionKey = defectId.ToString();
            this.RowKey = creationDate.ToString(RowKeyFormat);
            this.UsedID = userId;
            this.CommentText = commentText;
        }

        public CommentEntity() { }

        public int UsedID { get; set; }

        public string CommentText { get; set; }

    }

    public class TableStorageHelper
    {
        private const string _accountName = "AzureStorageAccount";

        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;

        public TableStorageHelper()
        {
            string appSetting = CloudConfigurationManager.GetSetting(_accountName);
            _storageAccount = CloudStorageAccount.Parse(appSetting);
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        public void InsertComment(int defectId, int userId, string text)
        {
            CloudTable table = _tableClient.GetTableReference("DefectComments");

            TableOperation insertOperation = TableOperation.Insert(new CommentEntity(DateTime.UtcNow, text, userId, defectId));
            table.Execute(insertOperation);
        }

        public IEnumerable<CommentViewModel> GetDefectComments(int defectId, AsignarDatabaseModel db)
        {
            CloudTable table = _tableClient.GetTableReference("DefectComments");
            string filter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, defectId.ToString());
            TableQuery<CommentEntity> query = new TableQuery<CommentEntity>().Where(filter);

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                IEnumerable<CommentViewModel> result = table.ExecuteQuery(query).Select(entity => new CommentViewModel
                {
                    CommentText = entity.CommentText,
                    CreationDate = DateTime.ParseExact(entity.RowKey, CommentEntity.RowKeyFormat, CultureInfo.InvariantCulture).ToLocalTime(),
                    UserPhoto = db.Users.First((u) => u.UserID == entity.UsedID).PhotoLink,
                    UserName = db.Users.First((u) => u.UserID == entity.UsedID).FirstName + " " +
                        db.Users.First((u) => u.UserID == entity.UsedID).Surname
                });

                dbContextTransaction.Commit();

                return result;
            }

        }
    }
}
