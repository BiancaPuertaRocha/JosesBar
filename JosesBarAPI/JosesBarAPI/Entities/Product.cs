using System.ComponentModel.DataAnnotations;

namespace JosesBarAPI.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public override string ToString()
        {
            return $"{this.Id};{this.Description};{this.Price};{this.Quantity};{this.CreatedAt}";
        }
        public static string GetHeader()
        {
            return "Id;Description;Price;Quantity;CreatedAt";
        }
    }
}
