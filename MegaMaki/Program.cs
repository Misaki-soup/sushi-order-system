using MegaMaki;
using MegaMaki.Core;
using MegaMaki.Data;

var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../MegaMaki.Data/menu.json"));
IMenuRepo repo = new JsonMenuRepo(path);

var ui = new ConsoleUi(repo);
ui.Run();