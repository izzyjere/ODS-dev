namespace ODS.Core.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }
    }
}
