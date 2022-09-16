using ODS.HelperModels;
using ODS.Wrapper;

namespace ODS.Services.Identity
{
    public interface IUserAccountService
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, int userId);
        Task<IResult<TokenResponse>> LoginAsync(TokenRequest<User> model);
        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest request, int userId);
        Task<IResult<List<UserResponse>>> GetAllUsersAsync();
        Task<IResult<Guid>> RegisterAsync(RegisterRequest request);

        Task<RegisterRequest> GetUserAsync(int id);
        Task<IResult> Delete(string email);
    }
}
