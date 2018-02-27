using System.Collections;
using System.Collections.Generic;
using GridMover;
using UnityEngine;

///Assign this script to you prefab that marks the selection on the map
[RequireComponent(typeof(Rigidbody))]
public class SelectGridMover : MonoBehaviour
{

    public class SelectMarker : QuadGridMover.Unit, GridMover.IUnit
    {
        float IUnit.GetSpeedOnTerrain(ILocation Location)
        {
            return 100f;  //Todo same speed on all tiles 
                          //except AFTER locked locations
        }

        IUnit IUnit.MakeDeepCopy()
        {
            return this; // Todo
        }
        
        float IUnit.GetRange()
        {
            return float.PositiveInfinity; //unlimited movement
        }

    }
    public class PlayerMarker : QuadGridMover.Unit, GridMover.IUnit
    {
        float IUnit.GetSpeedOnTerrain(ILocation Location)
        {
            return 100f;  //Todo same speed on all tiles except locked locations
        }

        IUnit IUnit.MakeDeepCopy()
        {
            return this; // Todo
        }

        float IUnit.GetRange()
        {
            return 1f; //may move only one tile
        }

    }
    public Rigidbody MarkerRigidBody;  //this is the selector avatar
    public Rigidbody PlayerRigidBody;  //this is the player avatar
    Animator m_Animator;
    public QuadGridMover m_Mover;
    SelectMarker m_SelectUnit; //this marks the actual selected node
    PlayerMarker m_PlayerUnit;  // this marks the node where the player will move to on confirm
    
    // Use this for initialization
    void Start()
    {
        m_PlayerUnit = new PlayerMarker();
        //PlayerRigidBody = this.GetComponent<Rigidbody>();
        m_PlayerUnit.Body = PlayerRigidBody;  
        
        m_SelectUnit = new SelectMarker();
        m_SelectUnit.Body = MarkerRigidBody;
        m_PlayerUnit.SelectPosition = m_SelectUnit.SelectPosition= transform.position;
        ((IGridMover)m_Mover).SetMap(null);

    }

    void Update()
    {
        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.
                                //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //Check if moving horizontally, if so set vertical to zero.
        if (horizontal != 0)
        {
            vertical = 0;
        }
        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0)
        {
            //Pass in horizontal and vertical as parameters to specify the direction to move Player in.
            m_Mover.AttemptMove(new Vector3(horizontal, 0, vertical), m_SelectUnit );
            m_Mover.AttemptMove(new Vector3(horizontal, 0, vertical), m_PlayerUnit);
        }
        //user ack the new position - move ahead
        if (Input.GetButtonDown("Fire1") && m_PlayerUnit.SelectPosition!=m_PlayerUnit.NewPosition) {
            m_PlayerUnit.NewPosition = m_PlayerUnit.SelectPosition; //this is now our new Startposition for Player
            m_SelectUnit.NewPosition = m_PlayerUnit.NewPosition;
        }
    }
}
