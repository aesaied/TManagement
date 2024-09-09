using System.ComponentModel.DataAnnotations.Schema;

namespace TManagement.Entities
{
    public class AppUserGroup
    {

        public Guid Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser? User { get; set; }


        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public AppGroup? Group { get; set; }
    }
}
