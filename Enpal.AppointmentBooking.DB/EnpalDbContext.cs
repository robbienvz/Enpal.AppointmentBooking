using Microsoft.EntityFrameworkCore;

namespace Enpal.AppointmentBooking.DB
{
    public class EnpalDbContext : DbContext
    {

        public EnpalDbContext(DbContextOptions<EnpalDbContext> options) : base(options) 
        {
        }
        public DbSet<SalesManager> SalesManagers { get; set; }
        public DbSet<Slot> Slots { get; set; }

    }
}
