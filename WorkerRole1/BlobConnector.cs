using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.IO;

namespace WorkerRole1
{
    public class BlobConnector
    {
        public BlobConnector()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("arimages");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();
        }

        public void UploadImage(string imageName, MemoryStream stream)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("arimages");

            // Retrieve reference to a blob named "Image1".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);

            // Create or overwrite the "Image1" blob with contents from a local file.
            blockBlob.UploadFromStream(stream);
        }
    }
}