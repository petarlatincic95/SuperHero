using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions<DataContext> options):base(options) //We must register this in program.cs
        {

        }
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
