using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCoreOwnedTypeBug
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(x => x
                    .AddConsole()
                    .AddFilter(y => y >= LogLevel.Debug)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Category>(entityBuilder =>
                {
                    entityBuilder.Property(x => x.Id).ValueGeneratedNever();
                });

            modelBuilder
                .Entity<ProductAttribute>(entityBuilder =>
                {
                    entityBuilder.Property(x => x.Id).ValueGeneratedNever();
                    entityBuilder.OwnsOne(x => x.Context);
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}