using Microsoft.EntityFrameworkCore;


namespace SerwisFilmowy.Database
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }
    }
}

