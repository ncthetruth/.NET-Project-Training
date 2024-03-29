using Microsoft.EntityFrameworkCore;

namespace Entity.Entity
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<AvailableTicket> AvailableTickets => Set<AvailableTicket>();
        public DbSet<BookTicket> BookTickets => Set<BookTicket>();
    }
}
