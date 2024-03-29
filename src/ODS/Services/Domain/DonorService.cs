﻿namespace ODS.Services.Domain
{
    public class DonorService : ServiceBase<Donor, int>
    {
        public DonorService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Donor> GetByUserId(Guid id)
        {
            return await Repository.Entities().Include(e => e.Donations).ThenInclude(d => d.Orphanage).Include(e => e.Payments).ThenInclude(p=>p.Orphanage).FirstOrDefaultAsync(u => u.UserId == id);
        }
    }
}
