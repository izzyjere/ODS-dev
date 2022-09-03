namespace ODS.Services.Domain
{
    public class PaymentService : ServiceBase<Payment, int>
    {
        public PaymentService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
