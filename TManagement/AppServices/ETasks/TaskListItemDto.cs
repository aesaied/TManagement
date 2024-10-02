using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TManagement.AppServices.ETasks
{
    public class TaskListItemDto
    {
        public int Id { get; set; }


     
        public string? Title { get; set; }


      

         public DateTime TaskDate { get; set; }

        public DateTime DueDate { get; set; }



        public TaskStatus CurrentStatus { get; set; }
        public bool HasAttachments { get; set; }


        public List<string> Users { get; set; }
    }
}