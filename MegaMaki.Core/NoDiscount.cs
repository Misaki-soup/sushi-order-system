namespace MegaMaki.Core;

public class NoDiscount : IDiscountStrategy
{
    public decimal Apply(decimal total) => total;
}