namespace MegaMaki.Core;

public class Order
{
    //gets the staff and makes a lot of staff in a list
    private List<OrderLine> MeWant {  get;  set; } = new();
    public Dictionary<string, List<OrderLine>> MyOrder { get; private set; } = new();
    
    
    public void addToList(OrderLine line) => MeWant.Add(line);
    
}
