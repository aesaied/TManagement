using TManagement.Entities;

namespace TManagement.AppServices.Loockups
{
    public class LoockupDto
    {

        public Guid Id { get; set; }

        public required string Name { get; set; }

        public Guid? FatherLookupId { get; set; }
    }
}