using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using HelpdeskSystem.Services;
using Microsoft.Extensions.Options;
using HelpdeskSystem.Models.User;
using Hangfire;
using Hangfire.SqlServer;
using HelpdeskSystem.Jobs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// appsettings.json > EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<EmailSettings>>().Value);

// Registrar o serviço que envia/recebe emails
builder.Services.AddScoped<IEmailTicketService, EmailTicketService>();

builder.Services.AddScoped<TicketPriorityJob>();

var app = builder.Build();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<TicketPriorityJob>(
    "escalar-prioridades",
    job => job.EscalarPrioridades(),
    Cron.Minutely // ou Cron.Minutely pra testar
);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHangfireDashboard();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();