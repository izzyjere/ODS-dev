namespace ODS.Domain.Interfaces
{
    public interface IUnitOfWork<TKey> : IDisposable
    {
        Task<int> Commit(CancellationToken cancellationToken);
        Task RollBack();
    }
}
