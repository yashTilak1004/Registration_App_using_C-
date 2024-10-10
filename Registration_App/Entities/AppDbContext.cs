using Microsoft.EntityFrameworkCore;
using Registration_App.Models;

namespace Registration_App.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base( options )//pass it to parent class 
        {

        }
        public DbSet<UA> Full_table  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Registration_App.Models.RegistrationViewModel> RegistrationViewModel { get; set; } = default!;
    }
}
//Data Source=DESKTOP-NAHT0BE\SQLEXPRESS;User ID=appuser;Password=Code@123;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False