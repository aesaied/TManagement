using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Mime;
using TManagement.AppServices.Attachments;
using TManagement.Entities;

namespace TManagement.Controllers
{
    public class AttachmentController(IAttachmentsAppService attachmentsAppService) : Controller
    {
        public async  Task<IActionResult> Index()
        {

          List<Attachment> attachments=await  attachmentsAppService.GetAll();
           

            return View(attachments);   
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


       // [Route("[Controller]/[Action]")]
        [Route("[Controller]/[Action]/{id}/{name}")]
        public async Task<IActionResult> ViewFile(Guid id, string? name)
        {

           var file= await attachmentsAppService.GetAttachmentToDownload(id);

            if (file != null && file.Content != null)
            {

                this.Response.Headers.ContentDisposition = "inline";
                return File(file.Content, file.ContentType, file.Name);

            }

            return NotFound();
        }
    }
}
