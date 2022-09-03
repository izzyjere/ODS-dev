using ODS.Enums;

namespace ODS.Models
{
    public class Donation : AuditableEntity<int>
    {

        public DateTime Date { get; set; }
        public int DonorId { get; set; }
        public string? Description { get; set; }          
        public int? Quantity { get; set; }
        public int OrphanageId { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Orphanage Orphanage { get; set; }
    }
}
