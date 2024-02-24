Created Class Hierachy - PlayerMovement, Enemy Spawn and Framework, Level Select and Framework done, Level Generation begun

GameClasses holds the top of hierarchy classes, Damageable and Managers. Damageable is anything that can attack, and be killed. Managers hold data from menus to create levels.
LevelManager extends GameClasses and hold essential data for each Level to Load. This is then extended by specific levels (Desert, Stones, Forest), which are will be added manually through level selection.
They hold Enemy GOs (GameObjects) and Obstacle GOs, which are then randomly generated based on parameters standardized by the LevelManager, and specified by the specific level objects.

The player can move and has a flippable sprite, and extends Damageable, meaning it can be hurt.
The enemy can also flip and take damage, but cannot yet pathfind
Level Generation takes in Obstacles in the Inspector and a size variable, and then loads Obstacles randomly within that range (negative and positive). 
As of the Week 7 lab, I was unable to add Density. I missed class Thursday and am confused on how to simulate density in that manner, especially given size restrictions.
