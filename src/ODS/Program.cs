using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ODS.Middleware;
using ODS.Services.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDatabase(connectionString);
builder.Services.AddServerServices(); 
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapPost("/api/payments",[Authorize] async (SystemDbContext context,[FromBody] Payment request) =>
{
    context.Set<Payment>().Add(request);
    var res = await context.SaveChangesAsync();
    return res > 0 ? Result.Success() : Result.Fail();
});
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SignInMiddleware<User>>();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.SeedDatabase();
app.Run();


