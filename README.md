# MegaMaki

Console ordering system for a takeaway sushi restaurant: menu management, orders with discounts, and order history.

## Prerequisites

- .NET 10 SDK
- An IDE: Rider or Visual Studio (VS Code also works)
- Nothing else to install. There is no database; data is stored in JSON files.

## Solution file

`MegaMaki.sln` in the repo root.

## Project structure

| Project | Layer | Contents |
|---|---|---|
| `MegaMaki` | Presentation | Console UI; `Program.cs` wires everything together |
| `MegaMaki.Core` | Domain | Menu items, orders, discounts (Strategy), repository interfaces |
| `MegaMaki.Data` | Persistence | JSON repositories, `menu.json`, `orders.json` |
| `MegaMaki.Test` | Tests | NUnit unit and integration tests |

## Diagrams

Class diagrams in Mermaid (they render directly on GitHub):
[`uml1.0.md`](uml1.0.md) in the repo root, which shows the early version first, then the final version.

## How to run

**In the IDE:** open `MegaMaki.sln`, set `MegaMaki` as the startup project, and run.

**From the terminal** (in the repo root):

```
dotnet run --project MegaMaki
```

`menu.json` already contains sample data, so you can place an order right away.

## How to test

**In the IDE:** open the Unit Tests window and click Run All.

**From the terminal** (in the repo root):

```
dotnet test
```

## Notes

- The data files are in `MegaMaki.Data/`. The app reads and writes them there directly.
- The app finds them relative to the standard build output (`bin/<config>/net10.0`), so run it from the IDE or with `dotnet run`, not as a published standalone app.
- Phone numbers use the Norwegian 8-digit format (`+47` and spaces are accepted and removed).