using MegaMaki;
using MegaMaki.Core;
using MegaMaki.Data;

var dataFolder = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "../../../../MegaMaki.Data"));

IMenuRepo menuRepo = new JsonMenuRepo(Path.Combine(dataFolder, "menu.json"));
IOrderRepo orderRepo = new JsonOrderRepo(Path.Combine(dataFolder, "orders.json"));

var ui = new ConsoleUi(menuRepo, orderRepo);
ui.Run();