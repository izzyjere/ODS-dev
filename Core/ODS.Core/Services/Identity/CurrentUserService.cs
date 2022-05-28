global using ODS.Core.Extensions;

namespace ODS.Core.Services.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        readonly IHttpContextAccessor contextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            contextAccessor = httpContextAccessor;
        }

        public Task<int> GetUserId()
        {
            var context = contextAccessor.HttpContext;
            if (context == null)
            {
                return Task.FromResult(0);
            }
            return Task.FromResult(context.User.GetId());
        }
#nullable disable

        public Task<string> GetUserName()
        {
            var context = contextAccessor.HttpContext;
            if (context == null)
            {
                return Task.FromResult("Unknown");
            }
            return Task.FromResult(context.User.Identity.Name);
        }
    }
}
