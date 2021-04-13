# 3D-A-Star-Pathfinding
This is a small repo I made while working on a game that needed node grid generation in 3 dimensions (multi level planes).
It was built in Unity but the principles stay the same across several languages. It is based off Sebastian Lague's tutorials
on A* in Unity, see https://www.youtube.com/watch?v=mZfyt03LDH4. Grid generation is the first and most important step--it is 
also the slowest. In my project, I had to generate grids with little node diameter which yielded a grid that was generated in
over 9 million iterations and 10 minutes in Unity Editor. It crashed my iPhone (duhh). As a result, I worked on, and almost 
completed, an optimization method:

Subdivision Testing:
This would generate the grid in two passes. First it would generate a subdivision grid with a node size that was much larger
than the final size. This would give a general idea of where nodes are concentrated (and as such where to generate nodes 
from). Then for each subdivision node, a grid node was generated. This is best suited for environments that are filled with 
open spaces. In my best tests, I was able to achieve less than 80,000 iterations.

Depth Aware Subdivision Testing:
This promises to reduce the grid generation operation from an O(n^3) operation to an O(n^2) operation by performing tests 
only on the XZ axis (looking at the environment top-down) and using raycasts to analyze all hits down the Y axis. This is 
currently incomplete but in my tests, I managed to reduce the iteration count to nearly 9,000.
