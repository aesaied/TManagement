using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TManagement.AppServices.ETasks
{
    public class TaskViewDto
    {

        public int? Id { get; set; }


        public string? Title { get; set; }


       
        public string? Description { get; set; }

         public DateTime TaskDate { get; set; }

        public DateTime DueDate { get; set; }


        public Entities.TaskStatus CurrentStatus { get; set; }





        public List<string> Users { get; set; }


        public List<AttachmentInfoDto>? Attachments { get; set; }


        
    }
}
