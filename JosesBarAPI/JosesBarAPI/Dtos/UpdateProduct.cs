using System.ComponentModel.DataAnnotations;

namespace JosesBarAPI.Dtos
{
    public class UpdateProduct
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description can not be empty.")]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        [Range(0.01, 99999999, ErrorMessage = "Price must be between 0.01 and 99999999.00")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be between 1 and 2147483647")]
        public int Quantity { get; set; }
    }
}
