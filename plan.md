### sys overview
Orders has mostly comming online. If taken by phone or F2F same system is used as for customers.
(if enough time - will do slughtly different system for cashier with ability of extra comments etc.) main logic is 
not changing. 

### Menu/cus selection
- sushi/menus/ drinks/ sides
- orders are editable:
  - change avokado to cucumber
  - extra soya/wasabi etc
  - for menues: change raw to fried
#### addons:
  extra payment for extra additions as soya etc.

### cus info 
- name
- phone
- (optional: time pick up)

### Order routing
- fried options go kitchen
- raw fish goes sushi rolling section
- drinks goes for kassa
- if order was edited from raw to fried

### discounts managment
- discounts can be set in case of need as low sales r comming. 
- costumer 5-10% club disc
- ansatt rabatt etc.

### orders channeling 
- `k` for kunde (customer who ordered in f2f)/phone preorders
- `w` web-order
- `t` Tablet orders in the salon area

### time managment

system  to destribute time for different orders:
- `w` orders has lower prioritycan takemore time to  be done
- `k` and c are higher prio. less time. auto distribution of the time at init of an order
not decreasing the time before older orders. setting 5 min less then the avarage time
  (avarage time is 20 min per order, can be set by the kitchen. `k` orders are  5 faster then avg)

### sys overview
Orders has mostly comming online. If taken by phone or F2F same system is used as for customers.
(if enough time - will do slughtly different system for cashier with ability of extra comments etc.) main logic is
not changing.

### Menu/cus selection
- sushi/menus/ drinks/ sides
- orders are editable:
  - change avokado to cucumber
  - extra soya/wasabi etc
  - for menues: change raw to fried
#### addons:
extra payment for extra additions as soya etc.

### cus info
- name
- phone
- (optional: time pick up)

### Order routing
- fried options go kitchen
- raw fish goes sushi rolling section
- drinks goes for kassa
- if order was edited from raw to fried

### discounts managment
- discounts can be set in case of need as low sales r comming.
- costumer 5-10% club disc
- ansatt rabatt etc.

### orders channeling
- `k` for kunde (customer who ordered in f2f)/phone preorders
- `w` web-order
- `t` Tablet orders in the salon area

### time managment

system  to destribute time for different orders:
- `w` orders has lower prioritycan takemore time to  be done
- `k` and c are higher prio. less time. auto distribution of the time at init of an order
  not decreasing the time before older orders. setting 5 min less then the avarage time
  (avarage time is 20 min per order, can be set by the kitchen. `k` orders are  5 faster then avg)

## done so far

### core
- menu item: name, price, station. no empty name, no negative price
- stations: kitchen / sushi / casa. every item knows where it goes (base for routing)
- order line: amount + item, amount cant be 0 or less
- order: list of lines, total, cus name + phone
  - phone: removes spaces, cuts `+47`, must be 8 numbers
- channels in code: `onCasa` = `k`, `Web` = `w`, `Salon` = `t`

### discounts
- changed from if/else with enum to strategy (`IDiscountStrategy`)
- no disc / club 10% / ansatt 20%
- order doesnt know which discounts exist, just calls `Apply`
- tests same numbers before and after (200/180/160) so nothing broke

### menu saving
- `IMenuRepo` in core, `JsonMenuRepo` in data → `menu.json`
- add / update / delete / get all
- cant add same name twice
- names not case sensitive ("maki" = "Maki")
- tests use temp files so real menu not touched

### console ui
- `Program.cs` only connects stuff, no console there
- manage menu: show, add, change price, delete (asks y/n)
- new order: channel → items (shows total) → discount → cus name + phone → receipt
- cus doesnt see station
- wrong input (letters, wrong number) doesnt crash, asks again
- price works with `,` and `.`

### found while testing
- tests were green but menu was empty in app, `menu.json` wasnt in run folder
- fixed with path to data folder. only works when run from rider / dotnet run

### not done yet
- editing orders (avokado → cucumber, raw → fried) + addons
- menu categories
- pick up time
- routing to stations (data is there, just not shown)
- time managment
- cashier version
- history:
  - `IOrderRepo` + `JsonOrderRepo` → `orders.json`
  - `OrderRecord` = simple class only for saving, cause order cant be loaded back from json
  - saves price at the time of order, so changing menu later doesnt change old orders
  - saves station per line for routing later
  - id = amount of orders + 1 (ok cause orders never deleted)
  - receipt shows order nr + date
  - new option in main menu: `3 Order history`
  - maybe: station tickets after receipt (kitchen / sushi / casa) = first step of routing