using ODS.Enums;

namespace ODS.Models;

public class Payment : AuditableEntity<int>
{
    public DateTime Date { get; set; }
    public int DonorId { get; set; }
    public int OrphanageId { get; set; }
    public string? Description { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string FWReference { get; set; }
    public string TransactionRef { get; set; }
    public virtual Donor Donor { get; set; }
    public virtual Orphanage Orphanage { get; set; }     

}
