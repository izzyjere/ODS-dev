namespace ODS.Core.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class, IEntity<TKey>
    {
        public Task Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
