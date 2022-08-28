namespace ODS.Interfaces
{
    public interface ICurrentUserService : IService
    {
        Task<string> GetUserName();
        Task<int> GetUserId();
    }
}
