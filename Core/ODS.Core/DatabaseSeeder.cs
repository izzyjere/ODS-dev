namespace ODS.Core
{
    public class DatabaseSeeder : ISeeder
    {
        SystemDbContext db;
        UserManager<User> userManager;
        RoleManager<Role> roleManager;
        ILogger<DatabaseSeeder> logger;
        public DatabaseSeeder(ILogger<DatabaseSeeder> logger,SystemDbContext db, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }
       
        public void Seed()
        {
            AddAdmin();
            db.SaveChanges();
        }

        private void AddAdmin()
        {
            Task.Run(async()=>
            {
                var superUser = await userManager.FindByEmailAsync("admin@ods.com");
                if (superUser is not null)
                {

                    //do nothing
                }
                else
                {
                    superUser = new User()
                    {
                        FirstName = "Nancy",
                        LastName = "Dee",
                        Email = "admin@ods.com",
                        EmailConfirmed = true,
                        UserName = "admin@ods.com",
                        PhoneNumber = "1234567890",
                        IsActive = true
                    };

                    var result = await userManager.CreateAsync(superUser, "test1234");
                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByNameAsync("Administrator");
                        if (role is null)
                        {
                            role = new Role("Administrator", "A Super User Role");
                            await roleManager.CreateAsync(role);
                        }
                        else { }                         
                        await userManager.AddToRoleAsync(superUser, "Administrator");
                        logger.LogInformation("Seeded Super User.");
                    }
                    else
                    {
                        logger.LogError(result.Errors.First().Description.ToString());
                    }
                    
                }
            }).GetAwaiter().GetResult();
        }
    }
}
