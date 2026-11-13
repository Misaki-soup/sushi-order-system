using MegaMaki.Core;

namespace MegaMaki.Test;

public class OrderLineTests
{
    [Test]
    public void GetTotalPrice_ThreeItemsAt25_Returns75()
    {
        var salmon = new MenuItem("Salmon nigiri", 25m, Station.Sushi);
        var line = new OrderLine(3, salmon);

        var result = line.GetTotalPrice(salmon);

        Assert.That(result, Is.EqualTo(75m));
    }

    [Test]
    public void GetTotalPrice_OneItem_ReturnsItemPrice()
    {
        var salmon = new MenuItem("Salmon nigiri", 25m, Station.Sushi);
        var line = new OrderLine(1, salmon);

        Assert.That(line.GetTotalPrice(salmon), Is.EqualTo(25m));
    }

    [Test]
    public void Constructor_ValidValues_KeepsCountAndProduct()
    {
        var salmon = new MenuItem("Salmon nigiri", 25m, Station.Sushi);
        var line = new OrderLine(2, salmon);

        Assert.That(line.Count, Is.EqualTo(2));
        Assert.That(line.Product.Name, Is.EqualTo("Salmon nigiri"));
    }

    [Test]
    public void Constructor_NegativeCount_Throws()
    {
        var salmon = new MenuItem("Salmon nigiri", 25m, Station.Sushi);

        Assert.Throws<ArgumentException>(() => new OrderLine(-1, salmon));
    }

    [Test]
    public void Constructor_ZeroCount_Throws()
    {
        var salmon = new MenuItem("Salmon nigiri", 25m, Station.Sushi);

        Assert.Throws<ArgumentException>(() => new OrderLine(0, salmon));
    }

    [Test]
    public void Constructor_NullItem_Throws()
    {
        Assert.Throws<ArgumentException>(() => new OrderLine(1, null));
    }
}