using System.IO;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.Auth;

namespace DAL
{
    public class BlobCreator : IBlobCreator
    {
        private readonly string accountName;
        private readonly string accountKey;
        private readonly string containerName;

        public BlobCreator(string accountName, string accountKey, string containerName)
        {
            this.accountName = accountName;
            this.accountKey = accountKey;
            this.containerName = containerName;
        }

        public BlobCreator()
        {
            accountName = "storage365";
            accountKey = "EetCLrk5/KQ1Y0hIOvm1lWrwtddjWWSXztsbFsEYOBt/agGnqD9zgRLZUTXOeLSbW/IeIdWJfqMwp0JBEeoRkQ==";
            containerName = "testcontainer";
        }

        public async Task<bool> Create(int userId, int taskId, string fileName, Stream stream)
        {
            string filePath = GetPath(userId, taskId, fileName);
            StorageCredentials storageCredentials = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filePath);

            await blockBlob.UploadFromStreamAsync(stream);

            return await Task.FromResult(true);
        }

        private string GetPath(int userId, int taskId, string fileName)
        {
            StringBuilder filePath = new StringBuilder("users/");
            filePath.Append(userId.ToString());
            filePath.Append("/tasks/");
            filePath.Append(taskId.ToString());
            filePath.Append("/" + fileName);

            return filePath.ToString();
        }
    }
}
