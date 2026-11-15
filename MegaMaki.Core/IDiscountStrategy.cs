namespace MegaMaki.Core;

public interface IDiscountStrategy
{
    decimal Apply(decimal total);
}