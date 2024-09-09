using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TManagement.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]

        public required string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string? PasswordHash { get; set; }

        [Required]
        [StringLength(500)]
        public string? PasswordSalt { get; set; }

        public UserStatus CurrentStatus { get; set; }

        public virtual ICollection<AppUserGroup>? Groups { get; set; }


        public Guid CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public Lookup? City { get; set; }



        public Guid EducationLevelId { get; set; }

        [ForeignKey(nameof(EducationLevelId))]
        public Lookup? EducationLevel { get; set; }





    }
}
