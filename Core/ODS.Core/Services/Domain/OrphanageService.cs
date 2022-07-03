namespace ODS.Core.Services.Domain
{
    public class OrphanageService : ServiceBase<Orphanage, int>
    {
        public OrphanageService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Orphanage> GetByEmail(string email)
        {
            return await Repository.Entities().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
