using Microsoft.EntityFrameworkCore;

namespace HomeAppStore.Models
{
	public class ProductContext : DbContext
	{
		public DbSet<Product> Products { get; set;}
		public DbSet<Order> Orders { get; set;}
        public DbSet<Client> Clients { get; set;}

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
