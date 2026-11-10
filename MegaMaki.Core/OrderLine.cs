namespace MegaMaki.Core;


public class OrderLine
{
    //make a kvitering basically
    public int Count { get; private set; }
    public MenuItem Product { get; set; }
    public OrderLine(int count,MenuItem item)
    {
        Count = count>0?count:throw new ArgumentException("amount cant be less then 0 ");
        Product = item != null?item:throw new ArgumentException("Cant be nothing");
        
    }

    public decimal GetTotalPrice()
    {
        return Product.Price * Count;
    }
}