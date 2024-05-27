using AzureBlopProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlopProject.Controllers
{
    public class BlobController : Controller
    {

        private readonly IBlobService _blobServices;

        public BlobController(IBlobService containerServices)
        {
            _blobServices = containerServices;
        }

        [HttpGet]
        public async Task<IActionResult> Manage(string containerName)
        {
            var blobObj = await _blobServices.GetAllBlobs(containerName);
            return View(blobObj);
        }
        
        [HttpGet]
        public async Task<IActionResult> AddFile(string containerName)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file, string containerName)
        {
            if(file == null || file.Length < 1) return View();
            
            var fileName = Path.GetFileNameWithoutExtension(file.FileName)+"_"+Guid.NewGuid()+"."+Path.GetExtension(file.FileName);

            var result = await _blobServices.UploadBlob(fileName, file, containerName);

            if(result)
                return RedirectToAction("Index", "Container");

            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> ViewFile(string name, string containerName)
        {
            return Redirect(await _blobServices.GetBlob(name, containerName));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFile(string name, string containerName)
        {
            await _blobServices.DeleteBlob(name, containerName);

            return RedirectToAction("Index", "Home");
        }
    }
}
