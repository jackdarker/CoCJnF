using System.Collections.Generic;
using UnityEngine;

namespace GridMover {
    //the player or enemy unit
    public interface IUnit {
        IUnit MakeDeepCopy();
        float GetSpeedOnTerrain(ILocation Location); //this is used for calculation of the best path
        //set it to 0 if the unit should not be able to move on this tile
        float GetRange(); //how many tiles can the unit move TODO actually every tile counts as 1
    }

    //a Tile in the map
    public interface ILocation {
        string nodeToString();
        Vector3 GetPosition();
    }
    //the collection of all tiles
    public interface IMap {
        //return the number of nodes between locations
        int GetDistance(ILocation a, ILocation b);
        //return neigbor-nodes around location
        ILocation[] GetNodeNeighbors(ILocation Node);
        ILocation GetNodeByPosition(Vector3 Pos);
    };

    public interface IGridMover {

        void SetMap(IMap Map); 
        //returns true if a satisfying path was found
        //even if false, the path might contain elements for display 
        bool GetPath(Vector3 from,Vector3 to,IUnit unit, out IList<ILocation> Path);

    }
}