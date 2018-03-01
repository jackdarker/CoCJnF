using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMover;


//represents a quadratic grid
//Vector 3.x = horizontal ; Vector3.z=vertical
class QuadGrid : IMap
{
    private int Width;
    private int Height;
    private ILocation[] Nodes;
    private static Dictionary<Location, GameObject> Markers = new Dictionary<Location, GameObject>();

    public QuadGrid()
    {

    }
    public QuadGrid(int width, int height)
    {
        Width = width;
        Height = height;
        ILocation _Node;
        Vector3 _Vector;
        Nodes = new ILocation[width*height];
        for (int z = 0; z != height; z++)
        {
            for (int x = 0; x != width; x++)
            {
                _Vector = new Vector3(x, 0, z);
                _Node = new Location(_Vector,_Vector.ToString());
                Nodes[x+Height*z]=_Node;
            }
        }
    }
    public int GetDistance(ILocation a, ILocation b)
    {        
        return (int)Math.Ceiling((a.GetPosition() - b.GetPosition()).magnitude); 
    }
    public ILocation GetNodeByPosition(Vector3 Pos) {
        int x = (int)Math.Ceiling(Pos.x);
        int z = (int)Math.Ceiling(Pos.z);
        if (0 <= x && x < Width && 0 <= z && z < Height)
        {
            return Nodes[x + z * Width];
        }
        else
        {
            return null;
        }

    }
    public ILocation[] GetNodeNeighbors(ILocation n)
    {
        List<ILocation> result= new List<ILocation>();
        ILocation _Node;
        Vector3 _V = n.GetPosition(); 
        //check in counterclockwise direction starting right
        _Node = GetNodeByPosition(_V+Vector3.right);
        if (_Node != null) result.Add(_Node);
        _Node = GetNodeByPosition(_V + Vector3.forward);
        if (_Node != null) result.Add(_Node);
        _Node = GetNodeByPosition(_V + Vector3.left);
        if (_Node != null) result.Add(_Node);
        _Node = GetNodeByPosition(_V + Vector3.back);
        if (_Node != null) result.Add(_Node);
        return result.ToArray();
    }



    // a Tile
    public class Location : ILocation, System.IEquatable<Location>
    {
        private Vector3 m_Pos;
        private string m_ID;
        public Location(Vector3 pos, string name)
        {
            this.m_Pos = pos;
            this.m_ID = name;
        }
        public Vector3 GetPosition()
        {
            return this.m_Pos;
        }
        public Vector3 Position
        {
            get
            {
                return this.m_Pos;
            }
            set
            {
                m_Pos = value;
            }
        }

        public string ID
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                m_ID = value;
            }
        }

        public override int GetHashCode()
        {
            return m_Pos.GetHashCode() ^ m_ID.GetHashCode();
        }
        public bool Equals(Location other)
        {
            return this.Equals(other.GetHashCode());
        }

        public string nodeToString()
        {
           return m_Pos.ToString();
        }
    }
    
    private class Pair<S, T>
    {
        private S first;
        private T second;

        public Pair(S first, T second)
        {
            this.first = first;
            this.second = second;
        }

        public S First { get { return this.first; } }

        public T Second { get { return this.second; } }
    }
}

