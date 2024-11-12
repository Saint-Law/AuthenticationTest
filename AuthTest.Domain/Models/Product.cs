﻿namespace AuthTest.Domain.Models;

public class Product : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
}
