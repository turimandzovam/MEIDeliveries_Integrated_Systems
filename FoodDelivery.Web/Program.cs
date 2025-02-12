using FoodDelivery.Domain.Identity;
using FoodDelivery.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FoodDelivery.Repository.implementation;
using FoodDelivery.Repository.Interface;
using FoodDelivery.Service.Interface;
using FoodDelivery.Service.Implementation;
using FoodDelivery.Domain.Payment;
using FoodDelivery.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Map Stripe Public and Secret Keys

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

// Configure DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddDbContext<TravelAgencyDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity with roles
builder.Services.AddDefaultIdentity<FoodDeliveryApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Add roles support
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddRazorPages();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddTransient<IDishService, DishService>();
builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IOrderService, OrderService>();

var app = builder.Build();

// Seed roles and users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRolesAndUsers(services); // Call method to seed roles and users
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure authentication is used
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

// Method to seed roles and an admin user
async Task CreateRolesAndUsers(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<FoodDeliveryApplicationUser>>();

    string[] roleNames = { "Admin", "User" };
    IdentityResult roleResult;

    foreach (var roleName in roleNames)
    {
        // Check if the role already exists; if not, create it
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Create an admin user
    var adminUser = new FoodDeliveryApplicationUser
    {
        UserName = "admin@fooddelivery.com",
        Email = "admin@fooddelivery.com",
        EmailConfirmed = true
    };

    string adminPassword = "Admin@123";
    var user = await userManager.FindByEmailAsync(adminUser.Email);

    if (user == null)
    {
        var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
        if (createPowerUser.Succeeded)
        {
            // Assign the admin role to the new user
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
