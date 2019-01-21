using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace BlobsUploadFile.HelperAzure
{
    public class BlobsManager
    {
        CloudStorageAccount storageAccount = null;
        CloudBlobContainer cloudBlobContainer = null;

        public BlobsManager()
        {
            var storageCredentials = new StorageCredentials("hperformance", "JYh2iZsUBacTBB+KnmBDhMsp9AGRce0zvzpx9aSzSdix44kPYhVV4tc7b1XBtGZH5C9pM9uPen016NXhL6DiBw==");
            storageAccount = new CloudStorageAccount(storageCredentials, true);
            
        }

        private async Task ConectionBlob()
        {
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {   
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);
        }
    }
}
