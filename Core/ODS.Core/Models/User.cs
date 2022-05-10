namespace ODS.Core.Models
{
    public class User : IdentityUser<int>
    {
        public virtual List<UserRole> Roles { get; set; }
    }
}
