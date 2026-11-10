namespace MegaMaki.Core;
using System.Text.RegularExpressions;

public class Order
{
    //gets the staff and makes a lot of staff in a list
    public List<OrderLine> MeWant {  get;  private set; } = new();
    public string cName {   get;  private set; }= "";
    public string phNum {   get;  private set; }= "";
    public decimal totalPrice { get; private set; } = 0m;
    public CameFrom channel {  get;  private set; }= CameFrom.onCasa;
    
    public Order(CameFrom ch) => channel = ch; //constr
    public void addToList(OrderLine line)
    {
        MeWant.Add(line);

        totalPrice = 0m;
        foreach (var item in MeWant)
        {
            totalPrice += item.GetTotalPrice(item.Product);
        }
    }
    
    public void customerCreds(string name,string phone)
    {
        cName = name;
        //preprocess phone input for val
        phone = phone.Replace(" ", ""); 
        phone = phone.StartsWith("+47") ? phone[3..] : phone; // muscle memory to enter +47 for some ppl 
        
        if (Regex.IsMatch(phone, @"^\d{8}$"))  //accepts input without +47
        {
            phNum = phone;
        }
        else
        {
            throw new ArgumentException("Must be 8 numbers and only numbers");
        }
        
    }

    //CameFrom later add orders ids 


}
