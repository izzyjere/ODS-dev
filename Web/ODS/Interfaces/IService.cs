global using ODS.Wrapper;

global using IResult = ODS.Wrapper.IResult;

namespace ODS.Interfaces
{
    public interface IService
    {
    }
    public interface IService<T, TKey> : IService where T : class, IEntity<TKey>
    {
        Task<IResult<T>> GetById(TKey id);
        Task<IResult<List<T>>> GetAll();

        Task<IResult> Create(T entity);

        Task<IResult> Update(T entity);

        Task<IResult> Delete(T entity);
    }
}
