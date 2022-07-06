using IResult= ODS.Domain.Wrapper.IResult;

namespace ODS.Core.Services
{
    public class ServiceBase<T, TKey> : IService<T, TKey> where T : class, IEntity<TKey>
    {
        protected IUnitOfWork<TKey> unitOfWork;
        public ServiceBase(IUnitOfWork<TKey> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        protected IRepository<T, TKey> Repository { get => unitOfWork.Repository<T>(); }

        public virtual async Task<IResult> Create(T entity)
        {
            try
            {
                await unitOfWork.Repository<T>().Add(entity);
                var result = await unitOfWork.Commit(new CancellationToken())!=0;
                return result ? await Result.SuccessAsync("Item Saved.") : await Result.FailAsync("An error occured while saving to the database");
            }
            catch (Exception e)
            {
                WriteLine(e.Message + e.StackTrace);
                return await Result.FailAsync("An error occured while saving to the database");

            }
        }

        public virtual async Task<IResult> Delete(T entity)
        {
            try
            {
                await unitOfWork.Repository<T>().Delete(entity);
                var result = await unitOfWork.Commit(new CancellationToken())!=0;
                return result ? await Result.SuccessAsync("Item Deleted.") : await Result.FailAsync("An error occured.");
            }
            catch (Exception e)
            {
                WriteLine(e.Message + e.StackTrace);
                return await Result.FailAsync("An error occured.");

            }
        }

        public virtual async Task<IResult<List<T>>> GetAll()
        {
            return await Result<List<T>>.SuccessAsync(await unitOfWork.Repository<T>().GetAll());
        }

        public virtual async Task<IResult<T>> GetById(TKey id)
        {
            var record = await unitOfWork.Repository<T>().Get(id);
            if (record == null)
            {
                return Result<T>.Fail("Not Found");
            }
            return await Result<T>.SuccessAsync(record);
        }

        public async Task<IResult> Update(T entity)
        {
            try
            {
                await unitOfWork.Repository<T>().Update(entity);
                var result = await unitOfWork.Commit(new CancellationToken())!=0;
                return result ? await Result.SuccessAsync("Item Updated.") : await Result.FailAsync("An error occured while saving to the database");
            }
            catch (Exception e)
            {
                WriteLine(e.Message + e.StackTrace);
                return await Result.FailAsync("An error occured while saving to the database");

            }
        }
    }
}
