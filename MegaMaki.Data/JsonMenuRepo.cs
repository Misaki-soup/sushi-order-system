using System.Text.Json;
using System.Text.Json.Serialization;
using MegaMaki.Core;

namespace MegaMaki.Data;

public class JsonMenuRepo : IMenuRepo
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,                          // readable JSON file
        PropertyNameCaseInsensitive = true,            // "name" and "Name" both work
        Converters = { new JsonStringEnumConverter() } // Station saved as "Sushi", not 1
    };

    public JsonMenuRepo(string filePath)
    {
        _filePath = filePath;
    }

    private List<MenuItem> Load()
    {
        if (!File.Exists(_filePath))
            return new List<MenuItem>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<MenuItem>>(json, Options) ?? new List<MenuItem>();
    }

    private void Save(List<MenuItem> items)
    {
        var json = JsonSerializer.Serialize(items, Options);
        File.WriteAllText(_filePath, json);
    }

    public List<MenuItem> GetAll() => Load();

    public void Add(MenuItem item)
    {
        var items = Load();

        if (items.Exists(i => i.Name.ToLower() == item.Name.ToLower()))
            throw new ArgumentException($"Menu item '{item.Name}' already exists");

        items.Add(item);
        Save(items);
    }

    public void Update(MenuItem item)
    {
        var items = Load();
        var index = items.FindIndex(i => i.Name.ToLower() == item.Name.ToLower());

        if (index == -1)
            throw new ArgumentException($"No menu item named '{item.Name}'");

        items[index] = item;
        Save(items);
    }

    public void Delete(string name)
    {
        var items = Load();
        items.RemoveAll(i => i.Name.ToLower() == name.ToLower());
        Save(items);
    }
}