

using ODS.Core.Factories;

namespace ODS.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddServerServices(this IServiceCollection services)
        {
            services
                .RegisterAutoMapper()
                .AddIdentity()
                .AddTransient<IUploadService, UploadService>()
                .AddIdentityServices()
                .AddMudServices(configuration =>
                {
                    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomEnd;
                    configuration.SnackbarConfiguration.HideTransitionDuration = 1000;
                    configuration.SnackbarConfiguration.ClearAfterNavigation = false;
                    configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                    configuration.SnackbarConfiguration.VisibleStateDuration = 5000;
                    configuration.SnackbarConfiguration.NewestOnTop = true;
                    configuration.SnackbarConfiguration.MaximumOpacity = 100;
                    configuration.SnackbarConfiguration.ShowCloseIcon = true;
                })
            .AddDomainServices();
           
            return services;
        }
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddAuthentication();
            services.AddAuthorization()
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<SystemDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IUserClaimsPrincipalFactory<User>, SystemUserClaimsFactory>();
            return services;
        }
    }
}
