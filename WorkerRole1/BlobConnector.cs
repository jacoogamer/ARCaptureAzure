// ***********************************************************************
// Assembly         : WorkerRole1
// Author           : Jason Coombes
// Created          : 07-20-2017
//
// Last Modified By : Jason Coombes
// Last Modified On : 07-24-2017
// ***********************************************************************
// <copyright file="BlobConnector.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The WorkerRole1 namespace.
/// </summary>
namespace WorkerRole1
{
    /// <summary>
    /// Class BlobConnector.
    /// </summary>
    public class BlobConnector
    {
        /// <summary>
        /// The storage account
        /// </summary>
        CloudStorageAccount storageAccount;

        /// <summary>
        /// The BLOB client
        /// </summary>
        CloudBlobClient blobClient;

        /// <summary>
        /// The container
        /// </summary>
        CloudBlobContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobConnector" /> class.
        /// </summary>
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

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="imageName">Name of the image.</param>
        /// <param name="stream">The stream.</param>
        public void UploadImage(string imageName, MemoryStream stream)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
            blockBlob.UploadFromStream(stream);
        }

        /// <summary>
        /// Downloads the image.
        /// </summary>
        /// <param name="imageName">Name of the image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] DownloadImage(string imageName)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
            blockBlob.FetchAttributes();
            long fileByteLength = blockBlob.Properties.Length;
            byte[] fileContent = new byte[fileByteLength];
            for (int i = 0; i < fileByteLength; i++)
            {
                fileContent[i] = 0x20;
            }
            blockBlob.DownloadToByteArray(fileContent, 0);
            return fileContent;
        }

        /// <summary>
        /// Lists the in BLOB.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
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

        /// <summary>
        /// Lists the blobs.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>List&lt;string&gt;.</returns>
        public List<string> ListBlobs(string containerName)
        {
            var backupContainer = blobClient.GetContainerReference(containerName);

            var blobs = backupContainer.ListBlobs();

            List<string> blobList = new List<string>();
            foreach (var blob in blobs.OfType<CloudBlob>())
            {
                string str = blob.Uri.ToString().Substring(blob.Uri.ToString().LastIndexOf('/')+1, blob.Uri.ToString().Length - blob.Uri.ToString().LastIndexOf('/')-1);
                blobList.Add(str);
            }

            return blobList;
        }

        /// <summary>
        /// Lists the BLOB directories.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>List&lt;string&gt;.</returns>
        public List<string> ListBlobDirectories(string containerName)
        {
            var backupContainer = blobClient.GetContainerReference(containerName);

            var blobs = backupContainer.ListBlobs();

            List<string> directory = new List<string>();

            foreach (var dir in blobs.OfType<CloudBlobDirectory>())
            {
                directory.Add(dir.Uri.ToString());
            }

            return directory;
        }

        /// <summary>
        /// Keys the exists.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Finds the by key.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="key">The key.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
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

        /// <summary>
        /// Deletes the BLOB.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="key">The key.</param>
        public void DeleteBlob(string containerName, string key)
        {
            var container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();

            var blockBlob = container.GetBlockBlobReference(key);
            blockBlob.Delete();
        }
    }
}