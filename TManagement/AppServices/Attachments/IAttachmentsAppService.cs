
namespace TManagement.AppServices.Attachments
{
    public interface IAttachmentsAppService
    {
        Task<AppResult> UploadAttachment(IFormFile file);
    }
}