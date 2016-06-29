using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AsignarServices.AzureStorage
{
    public static class BlobStorageHelper
    {
        public static List<string> GetPhotoUrls()
        {
            string appSetting = CloudConfigurationManager.GetSetting("BlobStorageAccount");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(appSetting);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("userphotos");

            List<string> result = new List<string>();
            List<string> blobNames = container.ListBlobs().OfType<CloudBlockBlob>().Select(b => b.Name).ToList();
            foreach (var blobName in blobNames)
            {
                result.Add(GetBlobSasUri(container, blobName));
            }

            return result;
        }

        public static string GetContainerSasUri(CloudBlobContainer container)
        {
            var sasPolicy = new SharedAccessBlobPolicy();
            sasPolicy.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasPolicy.Permissions = SharedAccessBlobPermissions.List;
            string sasContainerToken = container.GetSharedAccessSignature(sasPolicy);

            return container.Uri + sasContainerToken;
        }

        public static string GetBlobSasUri(CloudBlobContainer container, string blobName)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            var sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;
            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);

            return blob.Uri + sasBlobToken;
        }
    }
}
