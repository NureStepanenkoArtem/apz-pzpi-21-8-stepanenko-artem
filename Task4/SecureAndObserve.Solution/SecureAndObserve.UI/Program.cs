using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureAndObserve.Core.Domain.IdentityEntities;
using SecureAndObserve.Infrastructure.DbContext;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using SecureAndObserve.Core.ServiceContracts;
using SecureAndObserve.Core.Services;
using SecureAndObserve.Core.Domain.RepositoryContracts;
using SecureAndObserve.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {

    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IGuardExstensionsRepository, GuardExstensionsRepository>();
builder.Services.AddScoped<IRanksRepository, RanksRepository>();
builder.Services.AddScoped<ITerritoriesRepository, TerritoriesRepository>();
builder.Services.AddScoped<IOrderGuardsRepository, OrderGuardsRepository>();
builder.Services.AddScoped<IGuardReportRepository, GuardReportRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IEquipmentClaimsRepository, EquipmentClaimsRepository>();


builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IGuardExstensionsService, GuardExstensionsService>();
builder.Services.AddScoped<IRanksService, RanksService>();
builder.Services.AddScoped<ITerritoriesService, TerritoriesService>();
builder.Services.AddScoped<IOrderGuardsService, OrderGuardsService>();
builder.Services.AddScoped<IGuardReportsService, GuardReportsService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IEquipmentClaimsService, EquipmentClaimsService>();


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>((options) =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 3;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();


builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Login";
});

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});


var app = builder.Build();


if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseHttpLogging();

if (builder.Environment.IsEnvironment("Test") == false)
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

app.UseStaticFiles();



app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}");
});


app.Run();

public partial class Program { }
