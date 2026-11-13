using MegaMaki.Core;

namespace MegaMaki.Test;

public class MenuItemTests
{
    [Test]
    public void Constructor_ValidValues_KeepsPrice()
    {
        // Arrange + Act
        var item = new MenuItem("Salmon nigiri", 25m, Station.Sushi);

        // Assert
        Assert.That(item.Price, Is.EqualTo(25m));
    }

    [Test]
    public void Constructor_ValidValues_KeepsNameAndStation()
    {
        var item = new MenuItem("Miso soup", 40m, Station.Kitchen);

        Assert.That(item.Name, Is.EqualTo("Miso soup"));
        Assert.That(item.Station, Is.EqualTo(Station.Kitchen));
    }

    [Test]
    public void Constructor_NegativePrice_Throws()
    {
        Assert.Throws<ArgumentException>(() => new MenuItem("Salmon nigiri", -5m, Station.Sushi));
    }

    [Test]
    public void Constructor_ZeroPrice_IsAllowed()
    {
        // Arrange + Act
        var item = new MenuItem("Free water", 0m, Station.Casa);

        // Assert
        Assert.That(item.Price, Is.EqualTo(0m));
    }

    [Test]
    public void Constructor_EmptyName_Throws()
    {
        Assert.Throws<ArgumentException>(() => new MenuItem("", 25m, Station.Sushi));
    }
}