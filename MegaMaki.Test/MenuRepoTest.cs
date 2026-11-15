using MegaMaki.Core;
using MegaMaki.Data;

namespace MegaMaki.Test;

public class MenuRepositoryTest
{
    private string _filePath = "";
    private JsonMenuRepo _repo = null!;

    [SetUp]
    public void SetUp()
    {
        // fresh temp file path for every test
        _filePath = Path.Combine(Path.GetTempPath(), $"menu_test_{Guid.NewGuid()}.json");
        _repo = new JsonMenuRepo(_filePath);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_filePath))
            File.Delete(_filePath);
    }

    [Test]
    public void GetAll_NoFile_ReturnsEmptyList()
    {
        Assert.That(_repo.GetAll(), Is.Empty);
    }

    [Test]
    public void Add_ThenGetAll_ContainsItem()
    {
        _repo.Add(new MenuItem("Maki", 100m, Station.Sushi));

        var items = _repo.GetAll();

        Assert.That(items, Has.Count.EqualTo(1));
        Assert.That(items[0].Name, Is.EqualTo("Maki"));
        Assert.That(items[0].Price, Is.EqualTo(100m));
        Assert.That(items[0].Station, Is.EqualTo(Station.Sushi));
    }

    [Test]
    public void Add_IsSavedToFile_NewRepositoryCanReadIt()
    {
        _repo.Add(new MenuItem("Maki", 100m, Station.Sushi));

        var secondRepo = new JsonMenuRepo(_filePath);

        Assert.That(secondRepo.GetAll(), Has.Count.EqualTo(1));
    }

    [Test]
    public void Update_ExistingItem_ChangesPrice()
    {
        _repo.Add(new MenuItem("Maki", 100m, Station.Sushi));

        _repo.Update(new MenuItem("Maki", 150m, Station.Sushi));

        Assert.That(_repo.GetAll()[0].Price, Is.EqualTo(150m));
    }

    [Test]
    public void Update_UnknownItem_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _repo.Update(new MenuItem("Ghost", 50m, Station.Kitchen)));
    }

    [Test]
    public void Delete_RemovesOnlyThatItem()
    {
        _repo.Add(new MenuItem("Maki", 100m, Station.Sushi));
        _repo.Add(new MenuItem("Miso soup", 65m, Station.Kitchen));

        _repo.Delete("Maki");

        var items = _repo.GetAll();
        Assert.That(items, Has.Count.EqualTo(1));
        Assert.That(items[0].Name, Is.EqualTo("Miso soup"));
    }
    
}