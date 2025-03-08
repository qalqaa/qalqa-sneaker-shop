using Microsoft.AspNetCore.Identity;
using Autorisation.Endpoints;
using Microsoft.EntityFrameworkCore;
using qalqasneakershop.Data;
using qalqasneakershop.Data.Identity;
using Autorisation.Interfaces;
using Autorisation;
using Autorisation.Services;
using Autorisation.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Logging.AddConsole();

// Setup DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]!))
    };
});

// Authorization Policy
builder.Services.AddAuthorization(options => options.DefaultPolicy =
    new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());

// Identity
builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173",
                                              "https://localhost:5173",
                                              "https://localhost:8081/favourites")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                      });
});

// Configure JWT options
builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

// DbContext for ApplicationUser
builder.Services.AddDbContext<ApplicationUserDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

// Scoped services
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ISneakersRepository, SneakersRepository>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(DataBaseMappings));

builder.Services.AddControllers();

// Configure Kestrel to use HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    // Configure Kestrel to listen for HTTPS (with self-signed certs in development)
    options.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;


        // You can set up a certificate if needed for production, for example:
        // httpsOptions.ServerCertificate = new X509Certificate2("path/to/certificate.pfx", "password");
    });
});

var app = builder.Build();

// Автоматическое применение миграций
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        var userDbContext = services.GetRequiredService<ApplicationUserDbContext>();
        dbContext.Database.Migrate();
        userDbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Произошла ошибка при применении миграций.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection(); // Ensure HTTP requests are redirected to HTTPS
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        opt.RoutePrefix = "swagger";
    });
}

app.MapUsersEndpoints();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
