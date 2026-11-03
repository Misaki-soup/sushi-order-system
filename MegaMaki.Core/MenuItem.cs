namespace MegaMaki.Core;

public class MenuItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Station Station {get;set;}
    
    //constr
    public MenuItem(string name, decimal price, Station station)
    {
        Name = name.Length > 0? name: throw new ArgumentException("U cant have an item with no name");
        Price = price >= 0 ? price : throw new ArgumentException("price cant be less then 0 ");
        Station = station;
    }
}