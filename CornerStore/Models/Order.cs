
using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class Order
{
    public int Id { get; set; }
    [Required]
    public int CashierId { get; set; }
    public Cashier Cashier { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public decimal? Total {
        get
        {
            var res = OrderProducts.Select(p => p.Product.Price * p.Quantity );
            return res != null && res.Any() ? res.Sum() : (decimal?)0;
        }
    }
    public DateOnly? PaidOnDate { get; set; }
}