using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AsignarServices.AzureStorage
{
    public class BlobStorageHelper
    {
        private const string _accountName = "AzureStorageAccount";
        private const string _containerWithUserPhotosName = "userphotos";
        private const string _containerWithPriorityPhotosName = "defectpriorities";

        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobClient _blobClient;

        public BlobStorageHelper()
        {
            string appSetting = CloudConfigurationManager.GetSetting(_accountName);
            _storageAccount = CloudStorageAccount.Parse(appSetting);
            _blobClient = _storageAccount.CreateCloudBlobClient();
        }

        private static string GetContainerSasUri(CloudBlobContainer container)
        {
            var sasPolicy = new SharedAccessBlobPolicy();
            sasPolicy.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasPolicy.Permissions = SharedAccessBlobPermissions.List;
            string sasContainerToken = container.GetSharedAccessSignature(sasPolicy);

            return container.Uri + sasContainerToken;
        }

        private static string GetBlobSasUri(CloudBlobContainer container, string blobName)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            var sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;
            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);

            return blob.Uri + sasBlobToken;
        }

        public string GetUserPhotoUrl(int userId)
        {
            CloudBlobContainer userPhotoContainer = _blobClient.GetContainerReference(_containerWithUserPhotosName);
            string blobName = userPhotoContainer.ListBlobs(userId.ToString()).OfType<CloudBlockBlob>().Select(b => b.Name).FirstOrDefault();

            return GetBlobSasUri(userPhotoContainer, blobName);
        }

        public void UploadPhoto(int userId, string path)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithUserPhotosName);

            using (var fileStream = File.OpenRead(path))
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(Path.Combine(userId.ToString(), "photo.jpg"));
                blockBlob.UploadFromStream(fileStream);
            }
        }

        public Dictionary<int, string> GetDefectPriorityPhotos()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithPriorityPhotosName);
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    string blobName = directory.ListBlobs().OfType<CloudBlockBlob>().Select(b => b.Name).FirstOrDefault();

                    result.Add(Convert.ToInt32(directory.Prefix.Replace("/", "")), GetBlobSasUri(container, blobName));
                }
            }

            return result;
        }

        public Dictionary<int, string> GetUserPhotos()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithUserPhotosName);
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    string blobName = directory.ListBlobs().OfType<CloudBlockBlob>().Select(b => b.Name).FirstOrDefault();

                    result.Add(Convert.ToInt32(directory.Prefix.Replace("/", "")), GetBlobSasUri(container, blobName));
                }
            }

            return result;
        }

    }
}
