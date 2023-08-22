﻿using System.ComponentModel.DataAnnotations;

namespace JosesBarAPI.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Required]
        public DateTimeOffset Create_at { get; set; } = DateTimeOffset.Now;
        public int SellingCount { get; set; }
    }
}
