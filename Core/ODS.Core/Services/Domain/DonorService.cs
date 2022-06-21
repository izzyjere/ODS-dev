namespace ODS.Core.Services.Domain
{
    public class DonorService : ServiceBase<Donor, int>
    {
        public DonorService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Donor> GetByUserId(Guid id)
        {
            return await Repository.Entities().FirstOrDefaultAsync(u=>u.UserId == id);
        }
    }
}
