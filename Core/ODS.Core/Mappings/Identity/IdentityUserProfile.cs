namespace ODS.Core.Mappings.Identity
{
    public class IdentityUserProfile : Profile
    {
        public IdentityUserProfile()
        {
            CreateMap<UserResponse, User>().ReverseMap();
        }
    }
}