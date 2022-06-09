using Microsoft.EntityFrameworkCore;
using SerwisFilmowy.Entities;

namespace SerwisFilmowy.Database
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<MovieRate> MovieRates { get; set; }
    }

}
