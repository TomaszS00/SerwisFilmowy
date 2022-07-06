using SerwisFilmowy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SerwisFilmowy.Database;
using System.Reflection;
using System.Text;
using SerwisFilmowy;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SerwisFilmowy.Authorization;
using SerwisFilmowy.Entities;
using SerwisFilmowy.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Authorization
var authSettings = new AuthSettings();
builder.Configuration.GetSection("Authentication").Bind(authSettings);
builder.Services.AddSingleton(authSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authSettings.JwtIssuer,
        ValidAudience = authSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey)),
    };
});
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MovieDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add services to DI 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieCategoryService, MovieCategoryService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

PrepDB.PrepPopulation(app);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();