using System.ComponentModel.DataAnnotations.Schema;

namespace TManagement.Entities
{
    public class ETaskUsers
    {
        public Guid Id { get; set; }  
        public int TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]
        public ETask? Task { get; set; }


        public int UserId {  get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser? AssignedTo { get; set; } 

        public DateTime AssignDate { get; set; }

        public bool IsActive {  get; set; }
    }
}
