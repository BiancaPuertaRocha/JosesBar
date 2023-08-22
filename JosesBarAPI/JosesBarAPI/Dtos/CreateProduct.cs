using System.ComponentModel.DataAnnotations;

namespace JosesBarAPI.Dtos
{
    public class CreateProduct
    {
        [Required]
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
