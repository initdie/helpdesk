using helpdesk.Models;
using Microsoft.EntityFrameworkCore;


namespace helpdesk
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users{ get; set; }
    }
}
