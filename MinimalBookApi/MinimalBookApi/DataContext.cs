using Microsoft.EntityFrameworkCore;

namespace MinimalBookApi
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=Precision15\\SQLEXPRESS;Database=BookMinimalDB;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet<Book> books => Set<Book>();  
    }
}
