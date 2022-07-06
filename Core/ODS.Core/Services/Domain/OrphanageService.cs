namespace ODS.Core.Services.Domain
{
    public class OrphanageService : ServiceBase<Orphanage, int>
    {
        public OrphanageService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Orphanage> GetByEmail(string email)
        {
            return await Repository.Entities().Include(d=>d.Donations).ThenInclude(d=>d.Donor).Include(o=>o.Needs).FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<List<Orphanage>> GetAll()
        {
            return await Repository.Entities().Include(o=>o.Needs).ToListAsync();
        }
    }
}
