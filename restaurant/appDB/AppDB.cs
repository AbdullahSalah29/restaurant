using Microsoft.EntityFrameworkCore;
using restaurant.Models;

namespace restaurant.appDB
{
    public class AppDB : DbContext
    {
        public AppDB()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-433URB9;Initial Catalog=restaurant;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
  


        public AppDB (DbContextOptions<AppDB> options):base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Food> foods { get; set; }
        public DbSet<Order_Food> order_Foods { get; set; }
        public DbSet<Payment> payment { get; set; }
        public DbSet<Tabel> tabel { get; set; }




    }
}
