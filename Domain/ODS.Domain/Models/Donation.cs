global using ODS.Domain.Enums;

namespace ODS.Domain.Models
{
    public class Donation : AuditableEntity<int>
    {
     
        public DateTime Date { get; set; }
        public int DonorId { get; set; }
        public string? Description { get; set; }
        public DonationType Type { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }        
        public virtual Donor Donor { get; set; }
    }
}
