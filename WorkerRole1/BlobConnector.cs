using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace WorkerRole1
{
    public class BlobConnector
    {
        CloudStorageAccount storageAccount;
        CloudBlobClient blobClient;
        CloudBlobContainer container;

        public BlobConnector()
        {
            // Retrieve storage account from connection string.
            storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            container = blobClient.GetContainerReference("arimages");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();
        }

        public void UploadImage(string imageName, MemoryStream stream)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
            blockBlob.UploadFromStream(stream);
        }

        private void ListInBlob(string containerName)
        {
            var container = blobClient.GetContainerReference(containerName);
            if (!container.Exists())
            {
                container.Create();
            }

            foreach (var item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    var blob = (CloudBlockBlob)item;

                    Debug.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    var pageBlob = (CloudPageBlob)item;

                    Debug.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    var directory = (CloudBlobDirectory)item;

                    Debug.WriteLine("Directory: {0}", directory.Uri);
                }
            }
        }

        public bool KeyExists(string containerName, string key)
        {
            var container = blobClient.GetContainerReference(containerName);
            if (!container.Exists())
            {
                return false;
            }
            foreach (var item in container.ListBlobs(null, false))
            {
                if (item is CloudBlockBlob)
                {
                    var blob = (CloudBlockBlob)item;
                    if (blob.Name == key) return true;
                }
            }
            return false;
        }

        public List<string> FindByKey(string containerName, string key)
        {
            var container = blobClient.GetContainerReference(containerName);

            if (!container.Exists())
            {
                return new List<string>();
            }

            var keys = new List<string>();
            foreach (var item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    var blob = (CloudBlobDirectory)item;
                    if (blob.Uri.ToString().Contains(key))
                    {
                        keys.Add(blob.Uri.ToString());
                    }
                }
            }
            return keys;
        }

        private void DeleteBlob(string containerName, string key)
        {
            var container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();

            var blockBlob = container.GetBlockBlobReference(key);
            blockBlob.Delete();
        }
    }
}