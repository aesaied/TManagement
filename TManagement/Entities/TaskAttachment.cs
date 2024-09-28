using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TManagement.Entities
{
    public class TaskAttachment
    {



       
        public int TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]

        public ETask? Task { get; set; }
       
        public Guid AttachmentId { get; set; }

        [ForeignKey(nameof(AttachmentId))]
        public Attachment? Attachments { get; set; } 


        



    }
}
