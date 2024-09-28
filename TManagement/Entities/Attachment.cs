using System.ComponentModel.DataAnnotations;

namespace TManagement.Entities
{
    public class Attachment
    {

        public Guid Id { get; set; }

        [MaxLength(450)]
        [Required]

        public string? OriginalName { get; set; }

        [Required]
        public string? ContentType { get; set; }


        public long ContentLength { get; set; }

        [Required]
        public string? Path { get; set; }
    }
}
