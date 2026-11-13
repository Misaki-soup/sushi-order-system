using MegaMaki.Core;

namespace MegaMaki.Test;

public class OrderTest
{
    // --- Constructor ---

    [Test]
    public void Constructor_SetsChannel()
    {
        var order = new Order(CameFrom.Web);

        Assert.That(order.channel, Is.EqualTo(CameFrom.Web));
    }

    [Test]
    public void Constructor_NewOrder_IsEmptyWithZeroTotal()
    {
        var order = new Order(CameFrom.Salon);

        Assert.That(order.MeWant, Is.Empty);
        Assert.That(order.totalPrice, Is.EqualTo(0m));
    }

    // --- addToList ---

    [Test]
    public void AddToList_OneLine_AddsLineAndUpdatesTotal()
    {
        var salmon = new MenuItem("Salmon nigiri", 25m, Station.Sushi);
        var order = new Order(CameFrom.onCasa);

        order.addToList(new OrderLine(3, salmon));

        Assert.That(order.MeWant, Has.Count.EqualTo(1));
        Assert.That(order.totalPrice, Is.EqualTo(75m));
    }

    [Test]
    public void AddToList_TwoLines_TotalIsSumOfLines()
    {
        var salmon = new MenuItem("Salmon nigiri", 25m, Station.Sushi);
        var miso = new MenuItem("Miso soup", 40m, Station.Kitchen);
        var order = new Order(CameFrom.onCasa);

        order.addToList(new OrderLine(2, salmon)); // 50
        order.addToList(new OrderLine(1, miso));   // 40

        Assert.That(order.MeWant, Has.Count.EqualTo(2));
        Assert.That(order.totalPrice, Is.EqualTo(90m));
    }

    //customerCreds

    [TestCase("12345678")]
    [TestCase("123 45 678")]
    [TestCase("+4712345678")]
    [TestCase("+47 123 45 678")]
    public void CustomerCreds_ValidPhoneFormats_StoresEightDigits(string phone)
    {
        var order = new Order(CameFrom.Web);

        order.customerCreds("Ola", phone);

        Assert.That(order.phNum, Is.EqualTo("12345678"));
        Assert.That(order.cName, Is.EqualTo("Ola"));
    }

    [TestCase("1234567")]     // too short
    [TestCase("123456789")]   // too long
    [TestCase("1234abcd")]    // letters
    [TestCase("")]            // empty
    [TestCase("+4612345678")] // wrong country code
    public void CustomerCreds_InvalidPhone_Throws(string phone)
    {
        var order = new Order(CameFrom.Web);

        Assert.Throws<ArgumentException>(() => order.customerCreds("Ola", phone));
    }
}