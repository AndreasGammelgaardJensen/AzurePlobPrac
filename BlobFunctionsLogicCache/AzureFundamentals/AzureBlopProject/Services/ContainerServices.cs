using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlopProject.Services
{
    public class ContainerServices : IContainerServices
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ContainerServices(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task CreateContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        }

        public async Task DeleteContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainers()
        {
            List<string> containerName = new();


            await foreach (var blobContainer in _blobServiceClient.GetBlobContainersAsync()) {
                containerName.Add(blobContainer.Name);
            }
            return containerName;
        } 

        public async Task<List<string>> GetAllContainersAndBlobs()
        {
            List<string> containerAndBlobName = new();
            containerAndBlobName.Add("Account Nam  : " + _blobServiceClient.AccountName);
            containerAndBlobName.Add("_____________________________________________________________________________________________________________");
            await foreach(BlobContainerItem blobContainerItem in _blobServiceClient.GetBlobContainersAsync()) {
                containerAndBlobName.Add("--" + blobContainerItem.Name);

                BlobContainerClient _blobContainer = _blobServiceClient.GetBlobContainerClient(blobContainerItem.Name);

                await foreach(BlobItem blobItem in _blobContainer.GetBlobsAsync())
                {
                    containerAndBlobName.Add("--------" + blobItem.Name);
                }

                containerAndBlobName.Add("_____________________________________________________________________________________________________________");

            }

            return containerAndBlobName;
        }
    }
}
