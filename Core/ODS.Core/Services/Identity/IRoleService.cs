global using ODS.Domain.Wrapper;
global using ODS.Domain.HelperModels;

namespace ODS.Core.Services.Identity;

public interface IRoleService
{
    Task<Result<string>> AddEditAsync(RoleRequest request);
    Task<Result<string>> DeleteAsync(int id);
    Task<Result<List<RoleResponse>>> GetAllAsync();
    Task<Result<RoleResponse>> GetByIdAsync(int id);
    Task<int> GetCountAsync();
   
}