using MegaMaki.Core;

var salmon = new MenuItem("Salmon nigiri", 25m, Station.Sushi);
var cola = new MenuItem("Cola", 35m, Station.Casa);

var order = new Order(CameFrom.onCasa);
order.addToList(new OrderLine(3, salmon));
order.addToList(new OrderLine(1, cola));
order.customerCreds("Ola", "+47 912 34 567");

Console.WriteLine($"Customer: {order.cName}, {order.phNum}, {order.channel}");

foreach (var line in order.MeWant)
{
    Console.WriteLine($"{line.Count} x {line.Product.Name} = {line.GetTotalPrice(line.Product)}");
}

Console.WriteLine($"Total: {order.totalPrice} ");