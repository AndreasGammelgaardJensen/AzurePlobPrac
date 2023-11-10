using AzureBlopProject.Models;
using AzureBlopProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlopProject.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainerServices _containerServices;

        public ContainerController(IContainerServices containerServices)
        {
            _containerServices = containerServices;
        }

        public async Task<IActionResult> Index()
        {
            var allContainers = await _containerServices.GetAllContainers();
            
            return View(allContainers);
        }
        
        public async Task<IActionResult> Delete(string containerName)
        {
             await _containerServices.DeleteContainer(containerName);
            
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Create()
        {
            
            return View(new Container());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Container container)
        {
            await _containerServices.CreateContainer(container.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}
