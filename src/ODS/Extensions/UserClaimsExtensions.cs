﻿using System.Security.Claims;

namespace ODS.Extensions
{
    public static class UserClaimsExtensions
    {

        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        => Convert.ToInt32(claimsPrincipal.FindFirstValue("UserId"));
        public static int GetOrphanageId(this ClaimsPrincipal claimsPrincipal)
       => Convert.ToInt32(claimsPrincipal.FindFirstValue("OId"));
        public static Guid GetGuid(this ClaimsPrincipal claimsPrincipal)
        => Guid.Parse(claimsPrincipal.FindFirstValue("Guid"));
        public static string GetFirstName(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue("FirstName");
        public static string GetLastName(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue("LastName");
        public static string GetFullName(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.GetUserRole() != "Orphanage" ? claimsPrincipal.FindFirstValue("FullName") : claimsPrincipal.FindFirstValue("FirstName");
        public static string GetUserRole(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(ClaimTypes.Role);



    }
}
