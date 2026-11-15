namespace MegaMaki.Core;

// Plain data for saving a finished order
public class OrderRecord
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string CustomerName { get; set; } = "";
    public string Phone { get; set; } = "";
    public CameFrom Channel { get; set; }
    public List<OrderRecordLine> Lines { get; set; } = new();
    public string Discount { get; set; } = "";
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
}

public class OrderRecordLine
{
    public string Name { get; set; } = "";
    public int Count { get; set; }
    public decimal UnitPrice { get; set; }
    public Station Station { get; set; }
}