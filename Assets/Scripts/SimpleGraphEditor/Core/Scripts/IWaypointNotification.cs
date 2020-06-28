using UnityEngine;
using System.Collections;
using System.Collections.Generic;

  public abstract class IWaypointNotification :MonoBehaviour {
    public abstract void OnWaypointClicked(Waypoint source) ;
    public abstract bool OnWaypointReached(Waypoint source, GameObject Object);
}

