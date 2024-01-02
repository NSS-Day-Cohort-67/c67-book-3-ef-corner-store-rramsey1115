using CornerStore.Models;

public class OrderDTO
{
    public int Id { get; set; }
    public int CashierId { get; set; }
    public Cashier Cashier { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public decimal Total {
        get
        {
            decimal price = 0;
            var res = OrderProducts.Select(p => price += p.Product.Price * p.Quantity );
            return res != null && res.Any() ? res.Sum() : (decimal)0;
        }
    }
}