using ODS.Core.Services.Domain;

namespace ODS.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<DonationService>()
                    .AddTransient<OrphanageService>()
                    .AddTransient<OrphanageNeedService>()
                    .AddTransient<DonorService>();
            return services;
        }
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<AuditProfile>();               
                c.AddProfile<IdentityUserProfile>();                
                c.AddProfile<RoleProfile>();
            });
            services.AddSingleton(s => config.CreateMapper());
            return services;
        }
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAccountService, UserAccountService>()              
                    .AddScoped<IRoleService, RoleService>();
            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddHttpContextAccessor()
                    .AddScoped<ICurrentUserService, CurrentUserService>()
                    .AddDbContext<SystemDbContext>(optionsBuilder =>
                    {
                        optionsBuilder.UseSqlServer(connectionString, options =>
                        {
                            options.MigrationsAssembly(typeof(SystemDbContext).Assembly.FullName);
                            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                    }, ServiceLifetime.Transient)
                    .AddScoped<IUnitOfWork<int>, UnitOfWork<int>>()
                    .AddTransient<ISeeder, DatabaseSeeder>();
            return services;
        }
    }
}
