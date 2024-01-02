using CornerStore.Models;

public class ProductDTO
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}