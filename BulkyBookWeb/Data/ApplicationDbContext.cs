using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        //passing the DbContextOptions set in class ApplicationDbContext to the base class(DbContext)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        //Dbset (columns) of Model Class - Category
        //Table Name Categories
        public DbSet<Category> Categories { get; set; }
    }
}
