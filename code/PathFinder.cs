using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eses.PathFinder
{
    
    // Copyright Sami S.

    // use of any kind without a written permission 
    // from the author is not allowed.

    // DO NOT:
    // Fork, clone, copy or use in any shape or form.


    // NOTE: 
    // See comments above classes
    

    public class PathFinder
    {

        NodeData nodeData;
        Dictionary<Node, Node> visited;

        // ctor
        // Takes NodeData object which has
        // array + dict of map nodes preset
        public PathFinder(NodeData nodeData)
        {
            this.nodeData = nodeData;
        }

        // Call to calculate path
        public Dictionary<Node, Node> CalculateBreadthFirst(Node start, Node goal)
        {
            var posGoal = new Vector2(goal.x, goal.y);
            var frontier = new Queue<Node>();

            visited = new Dictionary<Node, Node>();

            // Start         
            frontier.Enqueue(start);
            visited[start] = null;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                var posCurrent = new Vector2(current.x, current.y);
                current.cost = (int)Vector2.Distance(posCurrent, posGoal);

                if (posCurrent == posGoal)
                {
                    break;
                }

                foreach (var next in current.neigbors)
                {
                    if (!visited.ContainsKey(next) && next.walkable)
                    {
                        frontier.Enqueue(next);
                        next.cameFrom = current;
                        visited[next] = current;
                    }
                }
            }

            return visited;
        }


        // Call to get path from A to B (if it exists)
        public List<Node> GetPath(Dictionary<Node, Node> walked, Node startNode, Node goalNode)
        {
            if (!walked.ContainsKey(goalNode))
            {
                goalNode = GetClosestWalked(walked, startNode);
                // Debug.LogWarning("No path to node");
            }

            List<Node> path = new List<Node>();

            var prev = goalNode;
            path.Add(prev);

            while (walked[prev] != null)
            {
                prev = walked[prev];
                path.Add(prev);
            }

            path.Reverse();

            return path;
        }


        // If there was no path, get closest node
        public Node GetClosestWalked(Dictionary<Node, Node> walked, Node start)
        {
            Node close = start;

            foreach (var n in walked.Keys)
            {
                if (n.cost < close.cost)
                {
                    close = n;
                }
            }

            // Debug.Log("Closest is: " + close.x + ":" + close.y);
            return close;
        }

    }


    // Node data class used in built graph
    public class NodeData
    {
        public Node[,] nodeArr;
        public Dictionary<Vector2Int, Node> nodeDict = new Dictionary<Vector2Int, Node>();

        // ctor
        public NodeData()
        {
        }

        // ctor
        public NodeData(int[,] mapIntArray)
        {
            MapDataIntToNodes(mapIntArray);

            foreach (var n in nodeArr)
            {
                SetNodeNeighbors(n);
            }

            // Create dictionary
            foreach (var n in nodeArr)
            {
                nodeDict.Add(new Vector2Int(n.x, n.y), n);
            }
        }


        // public ---------

        public Node GetNode(int x, int y)
        {
            if (x >= 0 && x <= nodeArr.GetLength(1) - 1 && y >= 0 && y <= nodeArr.GetLength(0) - 1)
            {
                return nodeArr[y, x];
            }

            return null;
        }



        // Helpers ---------


        // Set grid nodes
        void MapDataIntToNodes(int[,] mapData)
        {
            nodeArr = new Node[mapData.GetLength(0), mapData.GetLength(1)];

            for (int y = 0; y < mapData.GetLength(0); y++)
            {
                for (int x = 0; x < mapData.GetLength(1); x++)
                {
                    var node = new Node() { x = x, y = y };

                    // wall = 1
                    node.walkable = mapData[y, x] != 1 ? true : false;

                    nodeArr[y, x] = node;
                }
            }
        }

        // Neighbor data for Nodes
        void SetNodeNeighbors(Node node)
        {
            Node n = GetNode(node.x - 1, node.y);

            if (n != null)
                node.neigbors.Add(n);

            n = GetNode(node.x + 1, node.y);

            if (n != null)
                node.neigbors.Add(n);

            n = GetNode(node.x, node.y - 1);

            if (n != null)
                node.neigbors.Add(n);

            n = GetNode(node.x, node.y + 1);

            if (n != null)
                node.neigbors.Add(n);
        }

    }


    // Node of node graph
    public class Node
    {
        public int x;
        public int y;
        public int cost;
        public bool walkable;
        public Node cameFrom;
        public List<Node> neigbors = new List<Node>();
    }

}

