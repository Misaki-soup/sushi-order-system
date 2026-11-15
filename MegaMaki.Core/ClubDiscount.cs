namespace MegaMaki.Core;

public class ClubDiscount : IDiscountStrategy
{
    public decimal Apply(decimal total) => total * 0.90m;
}