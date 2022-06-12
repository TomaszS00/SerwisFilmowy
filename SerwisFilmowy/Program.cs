using SerwisFilmowy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SerwisFilmowy.Database;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();