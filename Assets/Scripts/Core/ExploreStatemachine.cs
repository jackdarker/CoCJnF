using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreStatemachine : StateMachine {

    public ButtonList m_SelButtons;
    public ButtonList m_NavButtons;
    public Clock m_Clock;
    public InfoBox m_Info;
    
    public SelectGridMover m_Mover;

    void Start () {
        ChangeState(new ExploreState(this));
	}
    void Update()
    {
        if (Input.GetButtonDown(InputController.k_Cancel)) {
            CurrentState.OnCancle();
        } else if (Input.GetButtonDown(InputController.k_Submit)) {
            CurrentState.OnSubmit();
        } else if (Input.GetButtonDown(InputController.k_Use)) {
          //  CurrentState.OnSubmit();
        } else {
            int horizontal = 0;     //Used to store the horizontal move direction.
            int vertical = 0;       //Used to store the vertical move direction.
                                    //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            //Check if moving horizontally, if so set vertical to zero.
            if (horizontal != 0) {
                vertical = 0;
            }
            //Check if we have a non-zero value for horizontal or vertical
            if (horizontal != 0 || vertical != 0) {
                CurrentState.MoveRelative(new Vector3(horizontal, 0, vertical), null);

            }
        }
        
    }
}
