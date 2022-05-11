namespace ODS.Core.Services.Domain
{
    public class DonationService : ServiceBase<Donation, int>
    {
        public DonationService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
