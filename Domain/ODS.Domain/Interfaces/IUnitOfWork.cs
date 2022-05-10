namespace ODS.Domain.Interfaces
{
    public interface IUnitOfWork<TKey> : IDisposable
    {
        IRepository<T, TKey> Repository<T>() where T : class, IEntity<TKey>;
        Task<int> Commit(CancellationToken cancellationToken);
        Task RollBack();
    }
}
