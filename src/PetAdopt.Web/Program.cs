using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data;
using PetAdopt.Identity;
using PetAdopt.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<PetAdoptDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity + Roles (using our AppUser and AppDbContext)
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false; // dev-only
        // you can tighten password rules later
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PetAdoptDbContext>();

// Authorization policy example (admin area)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages();
builder.Services.AddControllers(); // if/when needed

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

await Seed.EnsureAdminAsync(app.Services);  // <-- seeding (step 3.3)

app.Run();
