to do
---
make when key down space, the characher jump



done
---
make "Click Sereen to Start" blink





readme
---
we control the pokemon population based on the following three rules,


1. every 10 seconds, there are 50% chance that appears a new random pokemon, and they will be within player's sight.

2. pokemon that out of player's sight will be disappeared every 10 seconds.

3. there are at most 5 pokemon in the whole map.


basically, you will almost always have pokemon in your sight.

we believed that this is the best approaches as it removes any redunent elements as soon as it can while keeping pokemon appears on player's sight.



known bugs
- background shift speed drops when player X location < 800
(this is due to we do shifting by decreasing background canvas margin left value, 
so when X location is lower than window's width, the margin left doesnt)