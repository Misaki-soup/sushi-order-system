namespace MegaMaki.Core;

public interface IOrderRepo
{
    int Add(Order order);          // saves the order, returns its new id
    List<OrderRecord> GetAll();
}