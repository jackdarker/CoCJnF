using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMover;
using System;

public class QuadGridMover : MonoBehaviour,IGridMover
{
    public float moveTime = 0.1f;           //Time it will take object to move, in seconds.

    private float inverseMoveTime;			//Used to make movement more efficient.
    //private Rigidbody rbody;				//The Rigidbody component attached to this object.
    public abstract class Unit : GridMover.IUnit
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
        Vector3 m_SelectPosition;
        public Vector3 SelectPosition
        {
            get
            {
                return this.m_SelectPosition;
            }
            set
            {
                m_SelectPosition = value;
            }
        }
        Vector3 m_NewPosition;
        public Vector3 NewPosition
        {
            get
            {
                return this.m_NewPosition;
            }
            set
            {
                m_NewPosition = value;
            }
        }
        Rigidbody m_Body;
        public Rigidbody Body
        {
            get
            {
                return this.m_Body;
            }
            set
            {
                this.m_Body = value;
            }
        }
    }
    // Use this for initialization
    protected virtual void Start()
    {
        //Get a component reference to this object's Rigidbody2D
       // rbody = GetComponent<Rigidbody>();
        inverseMoveTime = 1f / moveTime;
    }
    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 end, Rigidbody rbody)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rbody.position, end, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rbody.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }
    //Move returns true if it is able to move and false if not. 
    //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
  /*  protected bool Move(IList<ILocation> Path)
    {
        //Store start position to move from, based on objects current transform position.
        if (Path.Count == 0) return false;
        Vector3 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector3 end = start + Path[0].GetPosition();

        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        boxCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        hit = Physics2D.Linecast(start, end, blockingLayer);

        //Re-enable boxCollider after linecast
        boxCollider.enabled = true;

        //Check if anything was hit
        //if ()hit.transform == null)
        {
            //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
            StartCoroutine(SmoothMovement(end));

            //Return true to say that Move was successful
            return true;
        }

        //If something was hit, return false, Move was unsuccesful.
        //return false;
    }*/
    //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
    //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
    public void AttemptMove(Vector3 dir,QuadGridMover.Unit unit) {
        //Hit will store whatever our linecast hits when Move is called.
        //RaycastHit hit;
        //Store start position to move from, based on objects current transform position.
        Vector3 start = unit.SelectPosition;// transform.position;
        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector3 end = start + dir;
        IList<ILocation> Path = new List<ILocation>();
        bool canMove = ((IGridMover)this).GetPath(start, end, unit, out Path); //cast required because explicit implementation?
        //bool canMove = ((QuadGridMover)this).GetPath(start, end, unit, out Path); //cast required because explicit implementation?
        //Physics.Raycast(start, dir, out hit);
        if (!canMove)
            //Call the OnCantMove function and pass it hitComponent as a parameter.
            OnCantMove();
        else
        {
            unit.SelectPosition = Path[Path.Count - 1].GetPosition(); 
            StartCoroutine(SmoothMovement(unit.SelectPosition,unit.Body)); 
            //Todo coroutine should process path to follow tril if more than 1 node
        }
        return;
        /*//Check if nothing was hit by linecast
        if (hit.transform == null)
            //If nothing was hit, return and don't execute further code.
            return;

        //Get a component reference to the component of type T attached to the object that was hit
        T hitComponent = hit.transform.GetComponent<T>();

        //If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
        if (!canMove && hitComponent != null)

            //Call the OnCantMove function and pass it hitComponent as a parameter.
            OnCantMove(hitComponent);*/
    }

    //The abstract modifier indicates that the thing being modified has a missing or incomplete implementation.
    //OnCantMove will be overriden by functions in the inheriting classes.
    protected void OnCantMove() { }

    private QuadGrid m_Map;
    void IGridMover.SetMap(IMap Map)
    {
        this.m_Map = new QuadGrid(20, 20);  //Todo how to load map
    }

    bool IGridMover.GetPath(Vector3 from, Vector3 to, IUnit unit, out IList<ILocation> Path)
    {
        /*  GridMover.AStar _Pathfinder = new GridMover.AStar(m_Map, m_Map.GetNodeByPosition(from),
              m_Map.GetNodeByPosition(to),unit);
          bool _Return= _Pathfinder.findPath();*/
        //Path = _Pathfinder.path;
        bool _Return = true;
        List<ILocation> _Path = new List<ILocation>();
        _Path.Add(m_Map.GetNodeByPosition(from));
        _Path.Add(m_Map.GetNodeByPosition(to));
        Path = _Path;
        return _Return;
    }
}
