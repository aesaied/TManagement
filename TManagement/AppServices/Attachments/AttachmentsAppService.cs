using AutoMapper;
using TManagement.Entities;

namespace TManagement.AppServices.Attachments
{
    public class AttachmentsAppService(AppDbContext dbContext, IMapper mapper, IConfiguration config) : IAttachmentsAppService
    {


        public async Task<AppResult> UploadAttachment(IFormFile file)
        {
            var originalName = file.FileName;

            var contentType = file.ContentType;
            var length = file.Length;

            MemoryStream stream = new MemoryStream();

            await file.CopyToAsync(stream);

            var randomName = System.IO.Path.GetRandomFileName();

            var extension = System.IO.Path.GetExtension(file.FileName);

            var newName = $"{randomName}{extension}";

            var basePath = config.GetValue<string>("AttachmentPath");

            var fullPath = System.IO.Path.Combine(basePath, newName);

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);

            }

            //


            await File.WriteAllBytesAsync(fullPath, stream.ToArray());

            Attachment attachment = new Attachment() { ContentLength = length, ContentType = contentType, OriginalName = originalName, Path = newName };

            dbContext.Attachments.Add(attachment);
            await dbContext.SaveChangesAsync();


            return AppResult.Ok();
            //
        }
    }
}
