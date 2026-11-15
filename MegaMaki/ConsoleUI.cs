using System.Globalization;
using MegaMaki.Core;

namespace MegaMaki;

public class ConsoleUi
{
    private readonly IMenuRepo _repo;

    public ConsoleUi(IMenuRepo repo)
    {
        _repo = repo;
    }

    //Main loop

    public void Run()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== MegaMaki ===");
            Console.WriteLine("1 Manage menu");
            Console.WriteLine("2 New order");
            Console.WriteLine("0 Exit");

            int choice = ReadInt("Choice: ");

            switch (choice)
            {
                case 1:
                    ManageMenu();
                    break;
                case 2:
                    NewOrder();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    //Manage menu

    private void ManageMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("--- Manage menu ---");
            Console.WriteLine("1 Show menu");
            Console.WriteLine("2 Add item");
            Console.WriteLine("3 Change price");
            Console.WriteLine("4 Delete item");
            Console.WriteLine("0 Back");

            int choice = ReadInt("Choice: ");

            switch (choice)
            {
                case 1:
                    ShowMenu();
                    break;
                case 2:
                    AddItem();
                    break;
                case 3:
                    ChangePrice();
                    break;
                case 4:
                    DeleteItem();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    // Prints the menu and returns the list, so other methods can pick from it
    private List<MenuItem> ShowMenu(bool showStation = true)
    {
        var items = _repo.GetAll();

        if (items.Count == 0)
        {
            Console.WriteLine("The menu is empty.");
            return items;
        }

        Console.WriteLine();
        if (showStation)
            Console.WriteLine($"{"Nr",3}  {"Name",-20} {"Price",10}  Station");
        else
            Console.WriteLine($"{"Nr",3}  {"Name",-20} {"Price",10}");

        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            if (showStation)
                Console.WriteLine($"{i + 1,3}  {item.Name,-20} {item.Price,7:0.00} kr  {item.Station}");
            else
                Console.WriteLine($"{i + 1,3}  {item.Name,-20} {item.Price,7:0.00} kr");
        }

        return items;
    }

    private void AddItem()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        decimal price = ReadDecimal("Price: ");

        var stations = Enum.GetValues<Station>();
        for (int i = 0; i < stations.Length; i++)
        {
            Console.WriteLine($"{i + 1} {stations[i]}");
        }
        int stationNr = ReadInt("Station number: ");

        if (stationNr < 1 || stationNr > stations.Length)
        {
            Console.WriteLine("No station with that number.");
            return;
        }
        Station station = stations[stationNr - 1];

        try
        {
            _repo.Add(new MenuItem(name, price, station));
            Console.WriteLine($"'{name}' added.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ChangePrice()
    {
        var items = ShowMenu();
        if (items.Count == 0) return;

        int nr = ReadInt("Item number: ");
        if (nr < 1 || nr > items.Count)
        {
            Console.WriteLine("No item with that number.");
            return;
        }

        var old = items[nr - 1];
        decimal newPrice = ReadDecimal($"New price for {old.Name}: ");

        try
        {
            _repo.Update(new MenuItem(old.Name, newPrice, old.Station));
            Console.WriteLine("Price updated.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void DeleteItem()
    {
        var items = ShowMenu();
        if (items.Count == 0) return;

        int nr = ReadInt("Item number: ");
        if (nr < 1 || nr > items.Count)
        {
            Console.WriteLine("No item with that number.");
            return;
        }

        string name = items[nr - 1].Name;
        Console.Write($"Delete '{name}'? (y/n): ");
        string answer = (Console.ReadLine() ?? "").Trim().ToLower();

        if (answer == "y")
        {
            _repo.Delete(name);
            Console.WriteLine("Deleted.");
        }
        else
        {
            Console.WriteLine("Cancelled.");
        }
    }

    //New order

    private void NewOrder()
    {
        //Channel
        var channels = Enum.GetValues<CameFrom>();
        for (int i = 0; i < channels.Length; i++)
        {
            Console.WriteLine($"{i + 1} {channels[i]}");
        }
        int channelNr = ReadInt("Channel number: ");
        if (channelNr < 1 || channelNr > channels.Length)
        {
            Console.WriteLine("No channel with that number.");
            return;
        }
        var order = new Order(channels[channelNr - 1]);

        //Items
        while (true)
        {
            var items = ShowMenu(false);
            if (items.Count == 0) return;

            int nr = ReadInt("Item number (0 = done): ");
            if (nr == 0) break;

            if (nr < 1 || nr > items.Count)
            {
                Console.WriteLine("No item with that number.");
                continue;
            }

            int qty = ReadInt("Quantity: ");

            try
            {
                order.addToList(new OrderLine(qty, items[nr - 1]));
                Console.WriteLine($"Running total: {order.totalPrice:0.00} kr");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Empty order
        if (order.MeWant.Count == 0)
        {
            Console.WriteLine("No items ordered, order cancelled.");
            return;
        }

        //Discount
        Console.WriteLine("1 None");
        Console.WriteLine("2 Club (10%)");
        Console.WriteLine("3 Employee (20%)");
        int discountNr = ReadInt("Discount: ");

        IDiscountStrategy discount;
        switch (discountNr)
        {
            case 2:
                discount = new ClubDiscount();
                break;
            case 3:
                discount = new EmployeeDiscount();
                break;
            default:
                discount = new NoDiscount();
                break;
        }
        order.SetDiscount(discount);

        //Customer
        Console.Write("Customer name: ");
        string customerName = Console.ReadLine() ?? "";

        while (true)
        {
            Console.Write("Phone (8 digits): ");
            string phone = Console.ReadLine() ?? "";
            try
            {
                order.customerCreds(customerName, phone);
                break;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Receipt
        Console.WriteLine();
        Console.WriteLine("========== RECEIPT ==========");
        Console.WriteLine($"Customer: {order.cName} ({order.phNum})");
        Console.WriteLine($"Channel:  {order.channel}");
        Console.WriteLine();
        foreach (var line in order.MeWant)
        {
            decimal lineTotal = line.GetTotalPrice(line.Product);
            Console.WriteLine($"{line.Count,3} x {line.Product.Name,-20} = {lineTotal,8:0.00} kr");
        }
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Subtotal:        {order.totalPrice,8:0.00} kr");
        Console.WriteLine($"After discount:  {order.GetTotalAfterDiscount(),8:0.00} kr");
        Console.WriteLine();
        Console.WriteLine("Press Enter to continue");
        Console.ReadLine();
    }

    //Input helpers

    private int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int result))
                return result;

            Console.WriteLine("Please enter a whole number.");
        }
    }

    private decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? "").Replace(',', '.');

            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
                return result;

            Console.WriteLine("Please enter a number, e.g. 99.50");
        }
    }
}