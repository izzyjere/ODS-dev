namespace ODS.Core.Services.Domain
{
    public class OrphanageService : ServiceBase<Orphanage, int>
    {
        public OrphanageService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
