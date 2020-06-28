using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// requires Camera with PhysicsRaycaster and Inputmodule, Collider on the waypoint
/// </summary>
public class WaypointClickable:Waypoint,IPointerClickHandler {


    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        this.getParent().SendMessage("WaypointClicked", this);
    }
}

