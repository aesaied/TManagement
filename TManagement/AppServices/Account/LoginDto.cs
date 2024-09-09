using System.ComponentModel.DataAnnotations;

namespace TManagement.AppServices.Account
{
    public class LoginDto
    {
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string? Email {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
