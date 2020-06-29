namespace EfCoreOwnedTypeBug
{
    public class ProductAttributeContext
    {
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}