using System.Text.Json;
using System.Text.Json.Serialization;
using MegaMaki.Core;

namespace MegaMaki.Data;

public class JsonOrderRepo : IOrderRepo
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public JsonOrderRepo(string filePath)
    {
        _filePath = filePath;
    }

    private List<OrderRecord> Load()
    {
        if (!File.Exists(_filePath))
            return new List<OrderRecord>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<OrderRecord>>(json, Options) ?? new List<OrderRecord>();
    }

    private void Save(List<OrderRecord> records)
    {
        var json = JsonSerializer.Serialize(records, Options);
        File.WriteAllText(_filePath, json);
    }

    public List<OrderRecord> GetAll() => Load();

    public int Add(Order order)
    {
        var records = Load();

        var record = new OrderRecord
        {
            Id = records.Count + 1,   // orders are never deleted, so this stays unique
            Date = DateTime.Now,
            CustomerName = order.cName,
            Phone = order.phNum,
            Channel = order.channel,
            Discount = order.Discount.GetType().Name,   // e.g. "ClubDiscount"
            Subtotal = order.totalPrice,
            Total = order.GetTotalAfterDiscount()
        };

        foreach (var line in order.MeWant)
        {
            record.Lines.Add(new OrderRecordLine
            {
                Name = line.Product.Name,
                Count = line.Count,
                UnitPrice = line.Product.Price,
                Station = line.Product.Station
            });
        }

        records.Add(record);
        Save(records);
        return record.Id;
    }
}