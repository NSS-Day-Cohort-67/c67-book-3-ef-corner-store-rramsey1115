using System.ComponentModel;
using CornerStore.Models;

public class OrderDTO
{
    public int Id { get; set; }
    public int CashierId { get; set; }
    public CashierDTO Cashier { get; set; }
    public List<OrderProductDTO> OrderProducts { get; set; }
    public decimal? Total {
        get
        {
            var res = OrderProducts.Select(p => p.Product.Price * p.Quantity );
            return res != null && res.Any() ? res.Sum() : (decimal?)0;
        }
    }
    public DateOnly? PaidOnDate { get; set; }
}