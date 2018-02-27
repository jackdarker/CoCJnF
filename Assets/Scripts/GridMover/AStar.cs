using System;
using System.Collections;
using System.Collections.Generic;

namespace GridMover
{
    public class AStar
    {
        private static float manhattanHeuristic(ILocation a, ILocation b, IMap graph, IUnit unit)
        {
            /* HACK: we add cost and subtract one to improve the situation in which the goal has a non-1 cost */
            return graph.GetDistance(a, b) + cost(a, b, unit) - 1;
        }

        // A function that takes two adjacent nodes and returns the
        // cost of traversing from one to the other. This function
        // does not have to be symmetric. Return Infinity if the
        // traversal is impossible.
        private static float cost(ILocation a, ILocation b, IUnit unit)
        {
            /* For now, we restrict costs to 1 or infinity, because we don't have a
             * good way to display all the relevant data visually (f, g, h,
             * cost, open, closed, parent pointer). */
            float sp = unit.GetSpeedOnTerrain(b);
            if (sp <= 0) return float.PositiveInfinity;
            float c = 100 * 100 / sp;
            if (float.IsNaN(c))
            {
                return float.PositiveInfinity;
            }
            else
            {
                return c;
            }
        }

        // Alpha can be between 0 (BFS) and 1 (Dijkstra's), with 0.5 being A*
        private float alpha = 0.4999f;

        // The VISITED set stores visited information about each node:
        // open, closed, parent, g, h, f. VISITED is the union of
        // CLOSED and OPEN.  We use a hash table (object) to represent
        // this set. The hash key is graph.nodeToString().  The parent
        // pointer inside VISITED points to the next object with
        // visited information (not to the node coordinates); this
        // makes it easy to reconstruct the path.
        private IDictionary<String, CalcNode> visited = new Dictionary<String, CalcNode>();

        // The OPEN set stores the subset of objects in 'visited'
        // that are currently open; we use an unsorted list.
        private Stack<CalcNode> open = new Stack<CalcNode>();

        // The path array stores the final path, once it's found
        public IList<ILocation> path = null; //Array of nodes
        private float pathcost = 0;

        /////////////////////////////////////////////
        public class CalcNode :System.IComparable<CalcNode>
        {
            public ILocation Node = null;
            public int Open = 0;
            public int Closed = 0;
            public CalcNode Parent = null;
            public float g = 0;
            public float h = 0;
            public float f = 0;
            public CalcNode() { }

            int IComparable<CalcNode>.CompareTo(CalcNode other)
            {
                return other.f.CompareTo(this.f);
            }
        }
        /////////////////////////////////////////////	
        private ILocation start;
        private ILocation goal;
        private IMap graph;
        private IUnit m_Unit;

        //Todo  we have to make a new object for every search ! Resuse it ! 
        public AStar(IMap graph, ILocation start, ILocation goal, IUnit unit)
        {
            this.graph = graph;
            this.start = start;
            this.goal = goal;
            this.m_Unit = unit.MakeDeepCopy(); 
            Initialize();
        }

        private void Initialize()
        {
            // Initialize the VISITED set and OPEN set by inserting
            // the start point

            CalcNode initialVisited = new CalcNode();
            initialVisited.Node = this.start;
            initialVisited.Open = 1;
            initialVisited.Closed = 0;
            initialVisited.Parent = null;
            initialVisited.g = 0;
            if (goal != null)
            {
                initialVisited.h = manhattanHeuristic(start, goal, graph, m_Unit);
                initialVisited.f = (alpha * initialVisited.g + (1 - alpha) * initialVisited.h) / Math.Max(alpha, 1 - alpha);
            }
            visited.Add(start.nodeToString(), initialVisited);
            open.Push(initialVisited);

        }
        private Stack<T> SortStack<T>(Stack<T> S1) where T : System.IComparable<T>
        {
            T x;
            Stack<T> S2 = new Stack<T>();
            while (S1.Count>0)
            {
                // Pop top element from stack S1
                x = S1.Pop();

                //Store x into S2 into its position in sorted stack
                while ((S2.Count > 0) && (S2.Peek().CompareTo(x)<0) )
                {
                    // Pop all the elements off the S2 that are 
                    // smaller than x and push them on S1
                    S1.Push(S2.Pop());
                }
                //push x on S2. Now x will be at its right position in sorted stack. 
                S2.Push(x);
            }
            return S2;
        }
        //returns if the path is found; variable path contains the path
        public bool findPath()
        {
            while (open.Count> 0)
            {
                // Find the best node (lowest f). After sorting it
                // will be the last element in the array, and we
                // remove it from OPEN and also update its open flag.
                //open = open.sortOn('f', Array.DESCENDING | Array.NUMERIC);
                open = SortStack<CalcNode>(open);

            CalcNode best = open.Pop();
            best.Open = 0;

            // If we find the goal, we're done.
            if (goal.Equals(best.Node)) {
                reconstructPath();
                return true;
            }

            // Add the neighbors of this node to OPEN
            ILocation[] next = graph.GetNodeNeighbors(best.Node);
            for (int j = 0; j != next.GetLength(0); j++)
            {
                float c = cost(best.Node, next[j], m_Unit);
                if (float.IsInfinity(c)) continue; // cannot pass

                // Every node needs to be in VISITED; be sure it's there.
                CalcNode e;
                if (!visited.TryGetValue(next[j].nodeToString(),out e) ) {
                    e = new CalcNode();
                    e.Node = next[j];
                    e.Open = 0;
                    e.Closed = 0;
                    e.Parent = null;
                    e.g = float.PositiveInfinity;
                    e.h = 0;
                    e.f = 0;
                    visited.Add(next[j].nodeToString(), e);
                }
                //if the movement-range of the unit is already used up we cannot reach target
                if ((this.m_Unit.GetRange() - (best.g + c)) < 0f) continue;
                    // We'll consider this node if the new cost (g) is
                    // better than the old cost. The old cost starts
                    // at Infinity, so it's always better the first
                    // time we see this node.
                    if (best.g + c < e.g)
                {
                    if (e.Open <= 0)
                    {
                        e.Open = 1;
                        open.Push(e);
                    }
                    e.g = best.g + c;
                    e.h = manhattanHeuristic(e.Node, goal, graph, m_Unit);
                    e.f = (alpha * e.g + (1 - alpha) * e.h) / Math.Max(alpha, 1 - alpha);
                    e.Parent = best;
                }
            }
        }
		return false;
	}
        /* returns all Tiles in Range - FIX IT !
         * public boolean findArea()
        {
            float fuel = m_Unit.getDistanceLeft();
            while (open.size() > 0)
            {
                CalcNode best = open.pop();
                best.open = 0;

                // Add the neighbors of this node to OPEN
                ArrayList<Node> next = graph.nodeNeighbors(best.node);
                for (int j = 0; j != next.size(); j++)
                {
                    float c = cost(best.node, next.get(j), m_Unit);
                    if (Float.isInfinite(c)) continue; // cannot pass

                    // Every node needs to be in VISITED; be sure it's there.
                    CalcNode e = visited.get(SquareGrid.nodeToString(next.get(j)));
                    if (e == null)
                    {
                        e = new CalcNode();
                        e.node = next.get(j);
                        e.open = 0;
                        e.closed = 0;
                        e.parent = null;
                        e.g = Float.POSITIVE_INFINITY;
                        e.h = 0;
                        e.f = 0;
                        visited.put(SquareGrid.nodeToString(next.get(j)), e);
                    }
                    if ((fuel - (best.g + c)) < 0f) continue;
                    // We'll consider this node if the new cost (g) is
                    // better than the old cost. The old cost starts
                    // at Infinity, so it's always better the first
                    // time we see this node.
                    if (best.g + c < e.g)
                    {
                        //trace(e.node.GetX() , e.node.GetY(),e.node.GetSlope(), e.g, e.h);
                        if (e.open <= 0)
                        {
                            e.open = 1;
                            open.push(e);
                        }
                        e.g = best.g + c;
                        e.parent = best;
                    }
                }
            }
            return false;
        }*/

        // Reconstruct the path from the goal back to the start ??
    private void reconstructPath()
    {
        path = new List<ILocation>();
        pathcost = 0;
            CalcNode pathVisited;
        while ((visited.TryGetValue(goal.nodeToString(), out pathVisited)) 
                && pathVisited.Node != start)
        {
            path.Insert(0, pathVisited.Node);
            pathcost += pathVisited.g;
            pathVisited = pathVisited.Parent;
        }
        path.Insert(0, start);
    }

/// //////////
/// </summary>
/// <param name="set"></param>
/// <param name="function"></param>
/// <returns></returns>
    //??
    private static ILocation getMin(HashSet<ILocation> set, Dictionary<ILocation, int> function)
        {
            int min = int.MaxValue;
            ILocation minHex = null;
            foreach (ILocation hex in set)
            {
                int value;
                if (function.TryGetValue(hex, out value))
                {
                    if (value <= min)
                    {
                        min = value;
                        minHex = hex;
                    }
                }
            }
            return minHex;
        }
        private static ILocation getMin(Dictionary<ILocation, int> function)
        {
            int min = int.MaxValue;
            ILocation minHex = null;
            foreach (KeyValuePair<ILocation, int> kvp in function)
            {
                if (kvp.Value <= min)
                {
                    min = kvp.Value;
                    minHex = kvp.Key;
                }
            }
            return minHex;
        }

        private static ILocation[] reconstructPath(Dictionary<ILocation, ILocation> cameFrom, ILocation final, int size)
        {
            ILocation[] path = new ILocation[size];
            path[size - 1] = final;
            for (int i = size - 2; i >= 0; --i)
            {
                cameFrom.TryGetValue(path[i + 1], out path[i]);
            }
            return path;
        }

        

        /*this is the basic algorythm ->
         * function A*(start,goal)
        closedset := the empty set    // The set of nodes already evaluated.
        openset := {start}    // The set of tentative nodes to be evaluated, initially containing the start node
        came_from := the empty map    // The map of navigated nodes.

        g_score[start] := 0    // Cost from start along best known path.
        // Estimated total cost from start to goal through y.
        f_score[start] := g_score[start] + heuristic_cost_estimate(start, goal)

        while openset is not empty
            current := the node in openset having the lowest f_score[] value
            if current = goal
                return reconstruct_path(came_from, goal)

            remove current from openset
            add current to closedset
            for each neighbor in neighbor_nodes(current)
                if neighbor in closedset
                    continue
                tentative_g_score := g_score[current] + dist_between(current,neighbor)

                if neighbor not in openset or tentative_g_score < g_score[neighbor] 
                    came_from[neighbor] := current
                    g_score[neighbor] := tentative_g_score
                    f_score[neighbor] := g_score[neighbor] + heuristic_cost_estimate(neighbor, goal)
                    if neighbor not in openset
                        add neighbor to openset

        return failure

    function reconstruct_path(came_from,current)
        total_path := [current]
        while current in came_from:
            current := came_from[current]
            total_path.append(current)
        return total_path*/
    }
}
