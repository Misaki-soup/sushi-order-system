using MegaMaki.Core;

namespace MegaMaki.Test;

public class DiscountTest
{
    private static Order MakeOrder()
    {
        var order = new Order(CameFrom.Web);
        order.addToList(new OrderLine(2, new MenuItem("Maki", 100m, Station.Sushi)));
        return order;
    }

    [Test]
    public void NoDiscount_ReturnsFullPrice()
    {
        var order = MakeOrder();

        Assert.That(order.GetTotalAfterDiscount(), Is.EqualTo(200m));
    }

    [Test]
    public void ClubDiscount_Gives10PercentOff()
    {
        var order = MakeOrder();
        order.SetDiscount(new ClubDiscount());

        Assert.That(order.GetTotalAfterDiscount(), Is.EqualTo(180m));
    }

    [Test]
    public void EmployeeDiscount_Gives20PercentOff()
    {
        var order = MakeOrder();
        order.SetDiscount(new EmployeeDiscount());

        Assert.That(order.GetTotalAfterDiscount(), Is.EqualTo(160m));
    }
}