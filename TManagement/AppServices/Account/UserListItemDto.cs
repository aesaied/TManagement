using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TManagement.Entities;

namespace TManagement.AppServices.Account
{
    public class UserListItemDto
    {
        public int Id { get; set; }

    

        public required string FullName { get; set; }

       
        public required string Email { get; set; }

      

        public UserStatus CurrentStatus { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }
        public string? EducationLevel { get; set; }


    }
}
