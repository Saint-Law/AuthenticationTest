

namespace AuthTest.Domain.Dtos.Product;

public class CreateProductDto
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
}
