using Microsoft.EntityFrameworkCore;

namespace WebApiRestaurant.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<User> Users => Set<User>();
    }
}
