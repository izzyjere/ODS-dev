namespace ODS.Core.Services.Domain
{
    public class DonorService : ServiceBase<Donor, int>
    {
        public DonorService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
