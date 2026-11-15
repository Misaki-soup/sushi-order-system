namespace MegaMaki.Core;

public interface IMenuRepo
{
    List<MenuItem> GetAll();
    void Add(MenuItem item);
    void Update(MenuItem item);
    void Delete(string name);
}