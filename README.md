# Pathfinding Breadth First (Unity / C#)

![Pathfinding image](/doc/pathfinding_breadth_first.gif)

## What is it?

This is one of the pathfinding experiments I have done - basically a set of classes that can be used for pathfinding using breadth first algorithm. I don't remember the reason why I didn't make the pathfinding use A* algorithm - but then again, I think I'm not using this pathfinding class currently. In the above GIF you can see one implemenation which I did using these classes together with Unity Tilemap feature and some custom tilemap drawing classes (implementation is not included here).

### Features:

* Generate tile based node graph from input integer array data

* Roam the graph

* Find a path between given start and goal nodes

* If no path exists to goal node, the closest walked path point can be queried

# Classes

## Pathfinder.cs
The main pathfinding class.

##  NodeData.cs
A class that contains the node data (nodes, neighbor info) and some helper methods to create the node graph.

## Node.cs
Single node in node graph.


# How to use
Here is an example how pathfinder can be used:

```C#
// Generate map data array from a bitmap (in this case, your mileage may vary)
int[,] array = PixelsToCells(mapTexture);

// Generate NodeData information from array
NodeData nodeData = new NodeData(array);

// Create new Pathfinder, takes NodeData
pathFinder = new PathFinder(nodeData);

// Set start and goal nodes
Node start = nodeData.GetNode(startPosition.x, startPosition.y);
Node goal = nodeData.GetNode(endPosition.x, endPosition.y);

// Create roaming data using start and goal as parameters
Dictionary<Node, Node> roamed = pathFinder.CalculateBreadthFirst(start, goal);

// Get calculated path from start to goal node
List<Node> path = pathFinder.GetPath(roamed, start, goal);

// Render animation (or do whatever you are doing)
// TilemapRenderer.instance.StartAnimations(roamed, path, null);
```

# About
I created this pathfinding system for myself, as a learning experience and it was supposed to be used for different personal Unity projects.

# Copyright 
Created by Sami S. use of any kind without a written permission from the author is not allowed. But feel free to take a look.
