using System.ComponentModel.DataAnnotations;

namespace JosesBarAPI.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset Create_at { get; set; } = DateTimeOffset.Now;
    }
}
