

using TManagement.Entities;

namespace TManagement.AppServices.Attachments
{
    public interface IAttachmentsAppService
    {
        Task<List<Attachment>> GetAll();
        Task<AttachmentFileInfo?> GetAttachmentToDownload(Guid id);
        Task<AppResult<Guid>> UploadAttachment(IFormFile file);
    }
}