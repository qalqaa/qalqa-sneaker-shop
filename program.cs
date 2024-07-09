using Microsoft.AspNetCore.Identity;
using Autorisation.Endpoints;
using Microsoft.EntityFrameworkCore;
using qalqasneakershop.Data;
using qalqasneakershop.Data.Identity;
using qalqasneakershop.Controllers;
using qalqasneakershop.Models;
using AutoMapper;
using Autorisation.Interfaces;
using Autorisation;
using Autorisation.Services;
using Autorisation.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddDbContext<ApplicationUserDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<UsersService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
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
    });
}

app.UseRouting();

app.UseCors(b =>
{
    b.WithOrigins("http://localhost:5173", "https://localhost:5173");
    b.AllowAnyHeader();
    b.AllowAnyMethod();
    b.AllowCredentials();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapUsersEndpoints();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
