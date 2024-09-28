using Microsoft.AspNetCore.Mvc;
using TManagement.AppServices.Attachments;

namespace TManagement.Controllers
{
    public class AttachmentController(IAttachmentsAppService attachmentsAppService) : Controller
    {
        public IActionResult Index()
        {
            return Content("Test Attachments");
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(IFormFile myFile)
        {


            await attachmentsAppService.UploadAttachment(myFile);

            return RedirectToAction("Index");
        }
    }
}
