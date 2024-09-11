using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TManagement.Entities;

namespace TManagement.AppServices.Account
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "FullName")]
        public required string FullName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        
        public required string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "ValRequired")]
        [StringLength(20)]
        [Compare(nameof(Password),ErrorMessage ="Confirm password doesn't match  password!")]
        [Display(Name = "ConfirmPassword")]
        public string? ConfirmPassword { get; set; }



        [Display(Name ="Country")]
        public Guid CountryId { get; set; }


        [Display(Name = "City")]

        public Guid CityId { get; set; }




        [Display(Name = "Education level")]

        public Guid EducationLevelId { get; set; }

      
    }
}
