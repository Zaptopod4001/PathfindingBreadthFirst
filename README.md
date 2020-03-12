# Pathfinding Breadth First (Unity / C#)

![Pathfinding image](/doc/pathfinding_breadth_first.gif)

## What is it?

This is the core class from one experiment I did - basically a set classes that can be used for Pathfinding using Breadth first. I don't remember the reason why I didn't make it use A* but then again, I think I'm not using this version currently. In the above gif, you see the implemenation I did using these classes (not included here, but it is not important).


Features

* Generate tile graph using nodes from input data

* Roam the graph 

* Find path between start node and goal node 

* If no path exists, the closest walked path point can be queried


# Classes

## Pathfinder.cs
The main pathfinding class.

##  NodeData.cs
A class that contains the node data (nodes, neighbor info) and some helper methods to create the node graph.

## Node.cs
Single node in node graph.


# How to use
Here is an example how pathfinder can be used:

```
// Generate mapdata from bitmap 
int[,] array = PixelsToCells(mapTexture);

// new nodeData from array data
NodeData nodeData = new NodeData(array);

// new Pathfinder, takes nodeData
pathFinder = new PathFinder(nodeData);

// Setup start and goal points
Node start = nodeData.GetNode(startPosition.x, startPosition.y);
Node goal = nodeData.GetNode(endPosition.x, endPosition.y);

// Get roaming data
Dictionary<Node, Node> roamed = pathFinder.CalculateBreadthFirst(start, goal);

// get path from start to goal
List<Node> path = pathFinder.GetPath(roamed, start, goal);

// Render animation (or do whatever)
// TilemapRenderer.instance.StartAnimations(roamed, path, null);
```


# About
I created this path finding  combination system for myself for different personal Unity projects. 

# Copyright 
Created by Sami S. use of any kind without a written permission from the author is not allowed. But feel free to take a look.