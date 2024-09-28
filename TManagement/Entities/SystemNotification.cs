using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TManagement.Services;

namespace TManagement.Entities
{
    public class SystemNotification
    {

        public Guid Id {  get; set; }


        public DateTime NotificationDate { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Message { get; set; }

        public NotificationType NotificationType { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser? AppUser { get; set; }

        [NotMapped] //  not save to  database
       public bool IsRead { get { return ReadDate.HasValue; } }

        public DateTime? ReadDate { get; set; }
        
    }
}
