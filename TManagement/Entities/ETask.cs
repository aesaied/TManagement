using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TManagement.Entities
{
    public class ETask
    {

        public int Id { get; set; }

        [Required]
        [StringLength(300,MinimumLength =3)]
        public string? Title { get; set; }


        [Column(TypeName ="ntext")]
        public string? Description { get; set; }

        public DateTime TaskDate { get; set; }

        public DateTime DueDate { get; set; }



        public ICollection<TaskAttachment>? Attachments { get; set; }
    }
}
