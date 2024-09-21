using TManagement.Entities;

namespace TManagement.AppServices.Account
{
    public class ChangeStatusDto
    {

        public int Id { get; set; }

        public UserStatus Status { get; set; }  
    }
}
