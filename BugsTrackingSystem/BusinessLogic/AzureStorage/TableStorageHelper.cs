using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using BugsTrackingSystem.Models;

namespace AsignarServices.AzureStorage
{
    public class CommentEntity : TableEntity
    {
        public CommentEntity(DateTime creationDate, string commentText, int userId, int defectId)
        {
            this.PartitionKey = defectId.ToString();
            this.RowKey = creationDate.ToString("yyyy-MM-dd HH:mm:ss");
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

        public void InsertComments()
        {
            CloudTable table = _tableClient.GetTableReference("DefectComments");

            TableOperation insertOperation0 = TableOperation.Insert(new CommentEntity(DateTime.UtcNow.AddHours(-5), "I write a first comment", 2, 1));
            TableOperation insertOperation1 = TableOperation.Insert(new CommentEntity(DateTime.UtcNow.AddHours(-4), "I write a second comment", 3, 1));
            TableOperation insertOperation2 = TableOperation.Insert(new CommentEntity(DateTime.UtcNow.AddHours(-3), "I write a third comment", 4, 1));
            TableOperation insertOperation3 = TableOperation.Insert(new CommentEntity(DateTime.UtcNow.AddHours(-2), "I write a fourth comment", 2, 1));
            TableOperation insertOperation4 = TableOperation.Insert(new CommentEntity(DateTime.UtcNow.AddHours(-1), "I write a fifth comment", 1, 1));
            TableOperation insertOperation5 = TableOperation.Insert(new CommentEntity(DateTime.UtcNow.AddHours(0), "I write a sixth comment", 2, 1));

            TableBatchOperation batchOperation = new TableBatchOperation();
            batchOperation.Insert(0, insertOperation0);
            batchOperation.Insert(0, insertOperation1);
            batchOperation.Insert(0, insertOperation2);
            batchOperation.Insert(0, insertOperation3);
            batchOperation.Insert(0, insertOperation4);
            batchOperation.Insert(0, insertOperation5);

            table.ExecuteBatch(batchOperation);
        }

        public IEnumerable<CommentViewModel> GetDefectComments(int defectId)
        {
            CloudTable table = _tableClient.GetTableReference("DefectComments");
            string filter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, defectId.ToString());
            TableQuery<CommentEntity> query = new TableQuery<CommentEntity>().Where(filter);

            IEnumerable<CommentViewModel> result = table.ExecuteQuery(query).Select(entity => new CommentViewModel
            {
                CommentText = entity.CommentText,
                CreationDate = DateTime.ParseExact(entity.RowKey, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToLocalTime()
            });

            return result;
        }
    }
}
