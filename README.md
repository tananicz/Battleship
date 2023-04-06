A very simple Battleship game simulator

the only thing that deserves a mention here is maybe the way battleships are distributed over the player's board.

When the ship is placed on a board not only its grids are selected, but also grids surrounding it are marked as kind of reserved
(think of it as something like drawing a line with anti-aliasing). I decided to use that solution because I didn't like how the ships were
glued to each other in the final result.

The procedure is as follows:

1. The orientation of a ship is first drawn (ie. horizontal or vertical) and useMargins flag is set to true to take into account reserved grids of other ships.

2. Let's assume that horizontal orientation was drawn in the previous step - the current one randomly selects some row of the board.

3. That row is then checked for "segments" of free space that would allow a new ship to be placed within - please observe that useMargins is set to true and 
"reserved" grids are treated the same way as if there was a ship present.

4. If the new ship can be placed within one of the segments it is placed there and the whole procedure is over. If however it is impossible to place our ship in any
segment, a new row number is selected from among the remaining ones and we go back to point 3.

5. If all the rows were examined for the possibility of placing a ship and no free space was found, the orientation is then switched to the other one (vertical in
our case) and we go back to point 2.

6. If there was no success in finding free space in both orientations, we go back to point 1. and repeat the two loops (for horizontal and vertical orientations).
This time however we set the useMargins flag to false and let our ship be glued to some other ship, as we know now there is no other way.

7. If the above procedure (both 4 possibilities with different directions and flag settings) failed, we throw an exception - maybe we need to configure the game 
with less ships or increase the size of our board.
