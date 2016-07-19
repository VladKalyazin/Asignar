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

        public string GetAttachmentUrl(int defectId, int attachmentId)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithAttachmentsName);

            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    if (directory.Prefix.Replace("/", "") == defectId.ToString())
                    {
                        foreach (var dirItem in directory.ListBlobs())
                        {
                            if (dirItem.GetType() == typeof(CloudBlobDirectory))
                            {
                                var subdir = (CloudBlobDirectory)dirItem;
                                if (subdir.Prefix == defectId.ToString() + '/' + attachmentId.ToString() + '/')
                                {
                                    foreach (var subdirItem in subdir.ListBlobs())
                                    {
                                        var blob = subdirItem as CloudBlockBlob;
                                        return GetBlobSasUri(container, blob.Name);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        public void UploadAttachment(int defectId, int attachmentId, byte[] byteFile, string name)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithAttachmentsName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(Path.Combine(defectId.ToString(), attachmentId.ToString(), name));
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

        public void DeleteAttachment(int attachmentId)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithAttachmentsName);

            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    foreach (var dirItem in directory.ListBlobs())
                    {
                        if (dirItem.GetType() == typeof(CloudBlobDirectory))
                        {
                            var subdir = (CloudBlobDirectory)dirItem;
                            if (subdir.Prefix.Contains('/' + attachmentId.ToString() + '/'))
                            {
                                foreach (var subdirItem in subdir.ListBlobs())
                                {
                                    var blob = subdirItem as CloudBlockBlob;
                                    blob.Delete();
                                }
                            }
                        }
                    }
                }
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

        public Dictionary<int, string> GetAttachments()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            CloudBlobContainer container = _blobClient.GetContainerReference(_containerWithAttachmentsName);

            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    foreach (var dirItem in directory.ListBlobs())
                    {
                        if (dirItem.GetType() == typeof(CloudBlobDirectory))
                        {
                            var subdir = (CloudBlobDirectory)dirItem;
                            foreach (var subdirItem in subdir.ListBlobs())
                            {
                                var blob = subdirItem as CloudBlockBlob;
                                result.Add(Convert.ToInt32(new String(subdir.Prefix.Reverse().TakeWhile(c => c!= '/').Reverse().ToArray())), GetBlobSasUri(container, blob.Name));
                            }
                        }
                    }
                }
            }

            return result;
        }

    }
}
