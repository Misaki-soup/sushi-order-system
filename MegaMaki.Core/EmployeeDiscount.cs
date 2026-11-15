namespace MegaMaki.Core;

public class EmployeeDiscount : IDiscountStrategy
{
    public decimal Apply(decimal total) => total * 0.80m;
}