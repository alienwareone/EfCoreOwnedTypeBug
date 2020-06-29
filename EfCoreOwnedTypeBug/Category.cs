using System.Collections.Generic;

namespace EfCoreOwnedTypeBug
{
    public class Category
    {
        public int Id { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}