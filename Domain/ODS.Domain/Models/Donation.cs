global using ODS.Domain.Enums;

namespace ODS.Domain.Models
{
    public class Donation : AuditableEntity<int>
    {
        public int OrphanageId { get; set; }
        public DateTime Date { get; set; }
        public int DonorId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public virtual Orphanage Orphanage { get; set; }
        public virtual Donor Donor { get; set; }
    }
}
