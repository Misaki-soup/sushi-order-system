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
- `k` for kunde (customer who ordered in f2f)
- `w` web-order
- `c` casa orders / phone preorders

### time managment

system  to destribute time for different orders:
- `w` orders has lower prioritycan takemore time to  be done
- `k` and c are higher prio. less time. auto distribution of the time at init of an order
not decreasing the time before older orders. setting 5 min less then the avarage time
  (avarage time is 20 min per order, can be set by the kitchen. `k` orders are  5 faster then avg)

