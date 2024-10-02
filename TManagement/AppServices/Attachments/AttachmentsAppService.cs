using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TManagement.Entities;

namespace TManagement.AppServices.Attachments
{
    public class AttachmentsAppService(AppDbContext dbContext, IMapper mapper, IConfiguration config) : IAttachmentsAppService
    {
        public async  Task<List<Attachment>> GetAll()
        {

            return await dbContext.Attachments.ToListAsync();
        }

        public async Task<AttachmentFileInfo?> GetAttachmentToDownload(Guid id)
        {
           var attachment =  await dbContext.Attachments.SingleOrDefaultAsync(x => x.Id == id);

            if (attachment == null)
            {
                return null;

            }


            var  output =  new AttachmentFileInfo() {  Name= attachment.OriginalName, ContentType=attachment.ContentType};

            var basePath = config.GetValue<string>("AttachmentPath");

            var fileFullPath = System.IO.Path.Combine(basePath, attachment.Path);
            if (File.Exists(fileFullPath))
            {

                output.Content =await File.ReadAllBytesAsync(fileFullPath);
            }

            return output;
        }

        public async Task<AppResult<Guid>> UploadAttachment(IFormFile file)
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


            return new AppResult<Guid>() { Success = true, Value = attachment.Id };
            //
        }
    }
}
