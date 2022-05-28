global using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ODS.Core.Middleware;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IResult = ODS.Domain.Wrapper.IResult;

namespace ODS.Core.Services.Identity
{
    public class UserAccountService : IUserAccountService
    {
        readonly UserManager<User> userManager;
        readonly SignInManager<User> signInManager;
        readonly ILogger<UserAccountService> logger;
        private readonly RoleManager<Role> roleManager; 
      
        readonly IMapper mapper;

        public UserAccountService(UserManager<User> _userManager,
                                  IMapper _mapper,
                                  SignInManager<User> _signInManager,
                                  ILogger<UserAccountService> _logger,
                                  RoleManager<Role> _roleManager)
                                 
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
            logger = _logger;
                      
            mapper = _mapper;

        }
        public async Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model)
        {
            if (model is null)
            {
                return await Result<TokenResponse>.FailAsync("Invalid Client Token");
            }
            var claimsPrincipal = GetPrincipalFromExpiredToken(model.Token);
            var userEmail = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByIdAsync(userEmail);
            if (user == null)
            {
                return await Result<TokenResponse>.FailAsync("User not found.");
            }
            if (user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return await Result<TokenResponse>.FailAsync("Invalid Client Token.");
            }
            var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));
            user.RefreshToken = GenerateRefreshToken();
            await userManager.UpdateAsync(user);
            var response = new TokenResponse() { Token = token, RefreshToken = user.RefreshToken, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime };
            return await Result<TokenResponse>.SuccessAsync(response);
        }
        private async Task<string> GenerateJwtAsync(User user)
        {
            var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));
            return token;
        }
        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            var permissionClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
                var thisRole = await roleManager.FindByNameAsync(role);
                var allPermissionsForThisRoles = await roleManager.GetClaimsAsync(thisRole);
                permissionClaims.AddRange(allPermissionsForThisRoles);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);

            return claims;
        }

        public async Task<IResult<TokenResponse>> LoginAsync(TokenRequest<User> tokenRequest)
        {
            try
            {
                var user = await userManager.FindByNameAsync(tokenRequest.UserName);
                if (user == null)
                {
                    return await Result<TokenResponse>.FailAsync("User Not Found.");
                }
                if (!user.IsActive)
                {
                    return await Result<TokenResponse>.FailAsync("User Not Active. Please contact the administrator.");
                }
                if (!user.EmailConfirmed)
                {
                    return await Result<TokenResponse>.FailAsync("Email not confirmed");
                }
                if (await signInManager.CanSignInAsync(user))
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user, tokenRequest.Password, true);
                    if (result.Succeeded)
                    {
                        var key = SignInMiddleware<User>.AnnounceLogin(tokenRequest);
                        user.RefreshToken = GenerateRefreshToken();
                        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                        await userManager.UpdateAsync(user);
                        var token = await GenerateJwtAsync(user);
                        var response = new TokenResponse { Token = token, TokenKey = key, RefreshToken = user.RefreshToken };
                        logger.LogInformation("User Trying to Log in..");
                        return await Result<TokenResponse>.SuccessAsync(response, $"Logged in as {user.UserName}.");
                    }
                    else
                    {
                        return await Result<TokenResponse>.FailAsync("Incorrect Credentials.");
                    }
                }
                else
                {
                    return await Result<TokenResponse>.FailAsync("Your Account Is Blocked. Please contact your group admin");
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message + e.StackTrace);
                return await Result<TokenResponse>.FailAsync("An Error Occured. Please Try Again");
            }




        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = Encoding.UTF8.GetBytes("JU5T50MERAND0M3SECRET");
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }
        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.UtcNow.AddDays(2),
               signingCredentials: signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);
            return encryptedToken;
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JU5T50MERAND0M3SECRET")),
                ValidateIssuer = false,
                ValidateAudience = false,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return await Result.FailAsync("User not Found");
            }
            var identityResult = await userManager.ChangePasswordAsync(
               user,
               model.Password,
               model.NewPassword);
            var errors = identityResult.Errors.Select(e => e.Description.ToString()).ToList();
            return identityResult.Succeeded ? await Result.SuccessAsync() : await Result.FailAsync(errors);

        }
        public async Task<IResult> UpdateProfileAsync(UpdateProfileRequest request, int userId)
        {
            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                var userWithSamePhoneNumber = await userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
                if (userWithSamePhoneNumber != null)
                {
                    return await Result.FailAsync(string.Format("Phone number {0} is already used.", request.PhoneNumber));
                }
            }

            var userWithSameEmail = await userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null || userWithSameEmail.Id == userId)
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return await Result.FailAsync("User Not Found.");
                }
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PhoneNumber = request.PhoneNumber;                 
                var phoneNumber = await userManager.GetPhoneNumberAsync(user);
                if (request.PhoneNumber != phoneNumber)
                {
                    var setPhoneResult = await userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
                }
                var identityResult = await userManager.UpdateAsync(user);
                var errors = identityResult.Errors.Select(e => e.Description.ToString()).ToList();
                await signInManager.RefreshSignInAsync(user);
                return identityResult.Succeeded ? await Result.SuccessAsync("User updated successfully") : await Result.FailAsync("An error occured.");
            }
            else
            {
                return await Result.FailAsync(string.Format("Email {0} is already used.", request.Email));
            }
        }
        public async Task<IResult> RegisterAsync(RegisterRequest request)
        {
            var userWithSameEmail = await userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail is not null)
            {
                return Result.Fail($"User with email {request.Email} already exists.");
            }
            var userWithSameUserName = await userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName is not null)
            {
                return Result.Fail($"User with username is already taken.");
            }
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                IsActive = true,
                EmailConfirmed = request.AutoConfirmEmail,               
            };
            var result = await userManager.CreateAsync(user, "kmc1234");
            if (result.Succeeded)
            {
                user.PhoneNumber = request.PhoneNumber;
                var phoneNumber = await userManager.GetPhoneNumberAsync(user);
                if (request.PhoneNumber != phoneNumber)
                {
                    await userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
                }               
                else { }
                await userManager.AddToRoleAsync(user, "BasicUser");
                return Result.Success("user created successifully.");
            }
            else                           
            {
                return Result.Success(result.Errors.First().Description.ToString());
            }

        }
        public async Task<RegisterRequest> GetUserAsync(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new RegisterRequest();
            }
            else
            {
                return new RegisterRequest
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    AutoConfirmEmail = user.EmailConfirmed,
                    UserName = user.UserName,
                    ActivateUser = user.IsActive
                };
            }
        }
        public async Task<IResult<List<UserResponse>>> GetAllUsersAsync()
        {
            var users = await userManager.Users.Include(u => u.Roles).ThenInclude(ur => ur.Role).ToListAsync();
            var response = new List<UserResponse>();
            foreach (var user in users)
            {
                response.Add(mapper.Map<UserResponse>(user));
            }
            return await Result<List<UserResponse>>.SuccessAsync(response);
        }
    }
}
