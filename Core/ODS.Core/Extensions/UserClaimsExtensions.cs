using System.Security.Claims;

namespace ODS.Core.Extensions
{
    public static class UserClaimsExtensions
    {

        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        => Convert.ToInt32(claimsPrincipal.FindFirstValue("UserId"));
        public static string GetFirstName(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue("FirstName");
        public static string GetLastName(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue("LastName");
        public static string GetFullName(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue("FullName");
        public static string GetUserRole(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(ClaimTypes.Role);

        
    }
}
