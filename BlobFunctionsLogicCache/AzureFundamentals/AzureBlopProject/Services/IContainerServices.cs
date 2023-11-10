namespace AzureBlopProject.Services
{
    public interface IContainerServices
    {
        Task<List<string>> GetAllContainersAndBlobs();
        Task<List<string>> GetAllContainers();
        Task CreateContainer(string containerName);
        Task DeleteContainer(string containerName);
    }
}
