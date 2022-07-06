namespace ODS.Core.Services.Domain
{
    public class OrphanageService : ServiceBase<Orphanage, int>
    {
        public OrphanageService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Orphanage> GetByEmail(string email)
        {
            return await Repository.Entities().Include(d=>d.Donations).ThenInclude(d=>d.Donor).Include(o=>o.OrphanageNeeds).FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<List<Orphanage>> GetAll()
        {
            return await Repository.Entities().Include(o=>o.OrphanageNeeds).ToListAsync();
        }
        public async Task<bool> IsEmailUsed(string email)
        {
            return await Repository.Entities().AnyAsync(o => o.Email == email);
        }
    }
}
