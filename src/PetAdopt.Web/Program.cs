using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Application.Repositories;
using PetAdopt.Application.Services;
using PetAdopt.Data;
using PetAdopt.Data.Identity;
using PetAdopt.Data.Repositories;
using PetAdopt.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();

// EF Core DbContext
builder.Services.AddDbContext<PetAdoptDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity + Roles + EF stores + Default UI
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<PetAdoptDbContext>()
.AddDefaultTokenProviders(); // safe to include

// Authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
});

// DI for repo/services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IAdoptionService, AdoptionService>();

var app = builder.Build();

// Errors + status code pages
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");

// Static + routing + auth
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages (includes Identity area)
app.MapRazorPages();

// Seed admin and sample data
await Seed.EnsureAdminAsync(app.Services);
// await SeedData.EnsureSampleDataAsync(app.Services);

app.Run();
