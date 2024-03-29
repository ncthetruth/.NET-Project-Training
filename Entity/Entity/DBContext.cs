using Microsoft.EntityFrameworkCore;

namespace Entity.Entity
{
	public class DBContext : DbContext
	{
        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Cart> Carts => Set<Cart>();
    }
}
