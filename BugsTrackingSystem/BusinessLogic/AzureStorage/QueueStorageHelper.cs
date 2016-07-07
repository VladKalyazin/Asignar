using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using BugsTrackingSystem.Models;
using System.Diagnostics;

namespace AsignarServices.AzureStorage
{
    public class QueueStorageHelper
    {
        private const string _accountName = "AzureStorageAccount";
        private const string _userQueueName = "usersqueue";

        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudQueueClient _queueClient;

        public QueueStorageHelper()
        {
            string appSetting = CloudConfigurationManager.GetSetting(_accountName);
            _storageAccount = CloudStorageAccount.Parse(appSetting);
            _queueClient = _storageAccount.CreateCloudQueueClient();
        }

        public void SendNewUserMessage(UserQueueModel user)
        {
            CloudQueue queue = _queueClient.GetQueueReference(_userQueueName);
            var message = new CloudQueueMessage(JsonConvert.SerializeObject(user));
            queue.AddMessage(message);
        }

    }
}
