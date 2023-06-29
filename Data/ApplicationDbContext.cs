using Microsoft.EntityFrameworkCore;
using RandomText.Models;

namespace RandomText.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server =(LocalDB)\\MSSQLLocalDB;database=RandomText;integrated security=true;");
        }

        public DbSet<RandomLetter> RandomLetters { get; set; }
    }
}
