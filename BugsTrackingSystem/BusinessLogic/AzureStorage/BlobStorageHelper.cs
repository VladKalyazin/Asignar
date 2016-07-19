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
        private const string _containerWithAttachmentsName = "defectattachments";

        private const int BlobLiveTimeInHours = 25;

        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobClient _blobClient;

        public BlobStorageHelper()
        {
            string appSetting = CloudConfigurationManager.GetSetting(_accountName);
            _storageAccount = CloudStorageAccount.Parse(appSetting);
            _blobClient = _storageAccount.CreateCloudBlobClient();
        }

        private static string GetBlobSasUri(CloudBlobContainer container, string blobName)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            var sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(BlobLiveTimeInHours);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;
            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);

            return blob.Uri + sasBlobToken;
        }

        public string GetUserPhotoUrl(int userId)
        {
            CloudBlobContainer userPhotoContainer = _blobClient.GetContainerReference(_containerWithUserPhotosName);
            //string blobName = userPhotoContainer.ListBlobs(Path.Combine(userId.ToString(), "photo.jpg")).OfType<CloudBlockBlob>().Select(b => b.Name).FirstOrDefault();
            CloudBlockBlob blockBlob = userPhotoContainer.GetBlockBlobReference(Path.Combine(userId.ToString(), "photo.jpg"));
            string blobName = blockBlob.Name;

            return GetBlobSasUri(userPhotoContainer, blobName);
        }

        public IEnumerable<string> GetAttachmentsUrls(int defectId)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithAttachmentsName);

            List<string> result = new List<string>();

            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    if (directory.Prefix.Replace("/", "") == defectId.ToString())
                    {
                        foreach (var dirItem in directory.ListBlobs())
                        {
                            if (dirItem.GetType() == typeof(CloudBlockBlob))
                            {
                                var blob = dirItem as CloudBlockBlob;
                                result.Add(GetBlobSasUri(container, blob.Name));
                            }
                        }
                        break;
                    }
                }
            }

            return result;
        }

        public void UploadAttachment(int defectId, byte[] byteFile, string name)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithAttachmentsName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(Path.Combine(defectId.ToString(), name));
            blockBlob.UploadFromByteArray(byteFile, 0, byteFile.Length);
        }

        public void UploadPhoto(int userId, byte[] byteImage)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithUserPhotosName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(Path.Combine(userId.ToString(), "photo.jpg"));
            blockBlob.UploadFromByteArray(byteImage, 0, byteImage.Length);
        }

        public void DeletePhoto(int userId)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithUserPhotosName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(Path.Combine(userId.ToString(), "photo.jpg"));
            blockBlob.Delete();
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
