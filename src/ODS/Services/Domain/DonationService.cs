using ODS.Enums;

namespace ODS.Services.Domain
{
    public class DonationService : ServiceBase<Donation, int>
    {
        public DonationService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Donation>> GetMyDonations(int donorId)
        {
            return await Repository.Entities().Where(d => d.DonorId == donorId).Include(d => d.Orphanage).ToListAsync();
        }
        public async Task<List<Donation>> GetAll(int orphanageId = 0)
        {
            var list = new List<Donation>();
            if (orphanageId > 0)
            {
                list = await Repository.Entities().Where(d => d.OrphanageId == orphanageId).Include(d => d.Orphanage).ToListAsync();
            }
            else
            {
                list = await Repository.Entities().Include(d => d.Orphanage).ToListAsync();
            }
            return list;
        }
        public override async Task<Wrapper.IResult> Create(Donation entity)
        {
            var item = await unitOfWork.Repository<OrphanageNeed>().Entities().FirstOrDefaultAsync(on => on.OrphanageId == entity.OrphanageId && on.Type == DonationType.Item && entity.Description.Contains(entity.Description, StringComparison.InvariantCultureIgnoreCase) && on.MonthStart.Value.Month == DateTime.Today.Month);
            if (item != null)
            {
                item.Raised += (double)entity.Quantity;
            }

            return await base.Create(entity);
        }
    }
}
