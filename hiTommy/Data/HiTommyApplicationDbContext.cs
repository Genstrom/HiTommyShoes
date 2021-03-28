using hiTommy.Data.Models;
using hiTommy.Models;
using Microsoft.EntityFrameworkCore;

namespace hiTommy.Data
{
    public class HiTommyApplicationDbContext : DbContext
    {
        public HiTommyApplicationDbContext(DbContextOptions<HiTommyApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Quantity> Quantities { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<OrderRows> OrderRows { get; set; }
    }
}