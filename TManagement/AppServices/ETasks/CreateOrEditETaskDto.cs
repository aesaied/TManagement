using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TManagement.Entities;

namespace TManagement.AppServices.ETasks
{
    public class CreateOrEditETaskDto
    {
        public int? Id { get; set; }


        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string? Title { get; set; }


        [Column(TypeName = "ntext")]
        public string? Description { get; set; }

      //  public DateTime TaskDate { get; set; }

        public DateTime DueDate { get; set; }



        [MinLength(1)]
        public IFormFile[]? Attachments { get; set; }


        public List<int> Users { get; set; }


        public List<AttachmentInfoDto>? OldAttachments { get; set; }


        public List<Guid>? OldAttachmentIds { get; set; }
    }
}
