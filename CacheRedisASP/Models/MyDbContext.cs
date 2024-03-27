using Microsoft.EntityFrameworkCore;

namespace CacheRedisASP.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}




 