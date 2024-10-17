using Microsoft.EntityFrameworkCore;
using WebAppBrightFlow.Models;

namespace WebAppBrightFlow.Data  
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}