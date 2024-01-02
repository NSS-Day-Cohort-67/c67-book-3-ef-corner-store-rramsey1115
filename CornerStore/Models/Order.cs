
using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class Order
{
    public int Id { get; set; }
    [Required]
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