using System.ComponentModel.DataAnnotations.Schema;

namespace TManagement.Entities
{
    public class Lookup
    {
        //  id =1 , Type =Country,name ='Palestine', Father=null
        public Guid Id { get; set; }

        public LookupType? Type { get; set; }
        public required string Name { get; set; }

        public Guid? FatherLookupId { get; set; }

        [ForeignKey(nameof(FatherLookupId))]
        public Lookup? FatherLookup {  get; set; }  


        public virtual ICollection<AppUser>? CityUsers { get; set; }
        public virtual ICollection<AppUser>? EducationLevelUsers { get; set; }



    }
}
