namespace ODS.Domain.Interfaces
{
    public interface IRepository<T, TKey> where T : class, IEntity<TKey>
    {
    }
}
