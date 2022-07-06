using Microsoft.EntityFrameworkCore;
using SerwisFilmowy.Database;
using SerwisFilmowy.Entities;


namespace SerwisFilmowy
{
    public class PrepDB
    {
        ublic static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<MovieDbContext>());
            }

            void SeedData(MovieDbContext context)
            {
                context.Database.Migrate();
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role()
                        {
                            Name = "User"
                        },
                        new Role()
                        {
                            Name = "Admin"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
