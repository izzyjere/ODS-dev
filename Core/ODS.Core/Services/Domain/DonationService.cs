namespace ODS.Core.Services.Domain
{
    public class DonationService : ServiceBase<Donation, int>
    {
        public DonationService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
        public override async Task<ODS.Domain.Wrapper.IResult> Create(Donation entity)
        {
            if(entity.Type == DonationType.Item)
            {
                var item = await unitOfWork.Repository<OrphanageNeed>().Entities().FirstOrDefaultAsync(on=>on.OpharnageId==entity.OrphanageId && on.Type==entity.Type&&entity.Description.Contains(entity.Description,StringComparison.InvariantCultureIgnoreCase) && on.MonthStart.Value.Month == DateTime.Today.Month);
                if(item!=null)
                {
                    item.Raised +=(double)entity.Quantity;
                }
            }
            else if (entity.Type == DonationType.Money)
            {
                var item = await unitOfWork.Repository<OrphanageNeed>().Entities().FirstOrDefaultAsync(on => on.OpharnageId == entity.OrphanageId && on.Type == entity.Type && on.MonthStart.Value.Month==DateTime.Today.Month);
                if (item != null)
                {
                    item.Raised += (double)entity.Amount;
                }
            }
           return await base.Create(entity); 
        }
    }
}
