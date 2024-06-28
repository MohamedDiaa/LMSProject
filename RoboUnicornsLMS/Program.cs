using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoboUnicornsLMS.Components;
using RoboUnicornsLMS.Components.Account;
using LMS.api.Model;
using LMS.api.Data;
using LMS.api.Seed;
using RoboUnicornsLMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(ApplicationPolicy.RequireAdminRole, policy => policy.RequireRole(ApplicationRole.Admin));
    options.AddPolicy(ApplicationPolicy.RequireTeacherRole, policy => policy.RequireRole(ApplicationRole.Teacher));
    options.AddPolicy(ApplicationPolicy.RequireStudentRole, policy => policy.RequireRole(ApplicationRole.Student));
    options.AddPolicy(ApplicationPolicy.RequireStudentOrTeacherRole, policy => policy.RequireRole(ApplicationRole.Student, ApplicationRole.Teacher));
    options.AddPolicy(ApplicationPolicy.RequireAnyRole, policy => policy.RequireAuthenticatedUser());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddRequestService<IActivityRequestService, ActivityRequestService>();
builder.Services.AddRequestService<ICourseRequestService, CourseRequestService>();
builder.Services.AddRequestService<IDocumentRequestService, DocumentRequestService>();
builder.Services.AddRequestService<IModuleRequestService, ModuleRequestService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

    await context.Database.MigrateAsync();

    await SeedData.InitAsync(context, userManager);

    var roles = new List<string> { ApplicationRole.Admin, ApplicationRole.Teacher, ApplicationRole.Student };

    foreach (var roleName in roles)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new ApplicationRole(roleName));
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
