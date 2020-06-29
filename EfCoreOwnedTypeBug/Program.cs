using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EfCoreOwnedTypeBug
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseSqlServer("Data Source = .\\SQLEXPRESS; Database = EfCoreOwnedTypeBug; Integrated Security = True;");

            var applicationDbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);
            applicationDbContext.Database.EnsureCreated();

            if (!applicationDbContext.ProductAttributes.Any())
            {
                applicationDbContext.Categories.Add(new Category { Id = 1 });

                applicationDbContext.ProductAttributes.AddRange(
                    new ProductAttribute { Id = 1, Context = new ProductAttributeContext { CategoryId = 1 } },
                    new ProductAttribute { Id = 2, Context = new ProductAttributeContext { CategoryId = 1 } },
                    new ProductAttribute { Id = 3, Context = new ProductAttributeContext { CategoryId = 1 } });

                applicationDbContext.SaveChanges();
            }

            var sql = applicationDbContext.ProductAttributes.ToQueryString();
            var result = applicationDbContext.ProductAttributes.ToList();

            var sql2 = applicationDbContext.Categories.Include(x => x.ProductAttributes).ToQueryString();
            var result2 = applicationDbContext.Categories.Include(x => x.ProductAttributes).ToList();
        }
    }
}