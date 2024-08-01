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
<<<<<<< HEAD
=======
using Autorisation.Extensions;
>>>>>>> 5aa180e2dbc20c7529301a4ee3e07dacfb38bda2
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authorization;
=======
>>>>>>> 5aa180e2dbc20c7529301a4ee3e07dacfb38bda2

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>() ?? throw new InvalidOperationException("JWT options not configured properly."); ;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Logging.AddConsole();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

<<<<<<< HEAD
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
builder.Services.AddAuthorization(optioins => optioins.DefaultPolicy =
            new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());

=======
>>>>>>> 5aa180e2dbc20c7529301a4ee3e07dacfb38bda2
builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173",
                                              "https://localhost:5173",
                                              "https://localhost:7228/favourites")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                      });
});

builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddApiAuthentication(jwtOptions);

builder.Services.AddDbContext<ApplicationUserDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ISneakersRepository, SneakersRepository>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddAutoMapper(typeof(DataBaseMappings));


builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
<<<<<<< HEAD

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});
=======
>>>>>>> 5aa180e2dbc20c7529301a4ee3e07dacfb38bda2

app.UseCors(MyAllowSpecificOrigins);

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapUsersEndpoints();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
