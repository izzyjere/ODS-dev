
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ODS.Core.Factories
{
    public class SystemUserClaimsFactory : UserClaimsPrincipalFactory<User, Role>
    {
        SystemDbContext context;
        public SystemUserClaimsFactory(SystemDbContext context,UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            this.context = context;
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {

            var identity = await base.GenerateClaimsAsync(user);
            var orphanage = await context.Set<Orphanage>().FirstOrDefaultAsync(o=>o.Email==user.Email);
            var oid = orphanage is null ? 0 : orphanage.Id;
            identity.AddClaim(new Claim("FullName", user.FirstName + " " + user.LastName));
            identity.AddClaim(new Claim("FirstName", user.FirstName));
            identity.AddClaim(new Claim("LastName", user.LastName));
            identity.AddClaim(new Claim("UserId", user.Id.ToString()));
            identity.AddClaim(new Claim("Guid", user.UserGuid.ToString()));
            identity.AddClaim(new Claim("OId", oid.ToString()));



            return identity;
        }
    }
}
