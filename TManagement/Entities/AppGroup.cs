using System.ComponentModel.DataAnnotations;

namespace TManagement.Entities
{
    public class AppGroup
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }


        public virtual ICollection<AppUserGroup>? Users { get; set; }


    }
}
