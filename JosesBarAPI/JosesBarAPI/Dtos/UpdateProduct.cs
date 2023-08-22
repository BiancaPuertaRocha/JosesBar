namespace JosesBarAPI.Dtos
{
    public class UpdateProduct
    {
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int SellingCount { get; set; }
    }
}
