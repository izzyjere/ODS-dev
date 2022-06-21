
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ODS.Core.Factories
{
    public class SystemUserClaimsFactory : UserClaimsPrincipalFactory<User, Role>
    {

        public SystemUserClaimsFactory(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {

            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FullName", user.FirstName + " " + user.LastName));
            identity.AddClaim(new Claim("FirstName", user.FirstName));
            identity.AddClaim(new Claim("LastName", user.LastName));
            identity.AddClaim(new Claim("UserId", user.Id.ToString()));
            identity.AddClaim(new Claim("Guid", user.UserGuid.ToString()));



            return identity;
        }
    }
}
