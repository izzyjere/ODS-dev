namespace ODS.Interfaces
{
    public interface IRepository<T, TKey> where T : class, IEntity<TKey>
    {
        IQueryable<T> Entities(bool eager = true);
        Task<List<T>> GetAll();
        Task<T> Get(TKey id);
        Task Delete(T entity);
        Task Update(T entity);
        Task Add(T entity);

    }
}
