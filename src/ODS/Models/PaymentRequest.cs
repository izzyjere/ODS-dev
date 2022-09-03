using ODS.Enums;

using static MudBlazor.Icons;

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
    public class PaymentResponse
    {
        public string Amount { get; set; }
        public string Currency { get; set; }
        public Customer Customer { get; set; }
        [JsonProperty("flw_ref")]   
        public string FWReference { get; set; }
        public string Stutus { get; set; }
        [JsonProperty("tx_ref")]
        public string TransactionReference { get; set; }
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }
    }
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

    }

}
