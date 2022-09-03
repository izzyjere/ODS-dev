using ODS.Enums;

namespace ODS.Models
{
    public class PaymentRequest
    {
        public PaymentMethod PaymentMethod { get; set; }
        public int DonorId { get; set; }
        public int OrphanageId { get; set; }
        public decimal Amount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TransactionRef { get; init; }
        public string Orphanage { get; internal set; }

        public PaymentRequest()
        {
            TransactionRef = Guid.NewGuid().ToString();
        }
    }   
}
