namespace ODS.Domain.Interfaces
{
    public interface IService
    {
    }
    public interface IService<T, TKey> : IService where T : class, IEntity<TKey>
    {
        Task<T> GetById(TKey id);
        Task<List<T>> GetAll();

        Task<bool> Create(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(T entity);
    }
}
