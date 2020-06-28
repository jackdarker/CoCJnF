using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// you have to pack your dialog into a derived class of this one
/// </summary>
public class DialogSceneData : MonoBehaviour
{

    public DialogSceneData() {

    }

    /// <summary>
    /// unique identifier for this scene for example as reference in save file; todo make this a real UID ??
    /// </summary>
    virtual public int GetUId() {
        return 0;           //Todo force overriding this
    }
    /// <summary>
    /// called on starting of display-dialog
    /// </summary>
    virtual public void Setup() {

    }
    /// <summary>
    /// gets called from display dialog whenever a button is pressed to switch to the next dialog
    /// You have to build a statemachine that returns a dialogtree containing what to display and what choices the player has.
    /// The choices have a Choice-ID (unique to this statemachine) and will be fed back by SetDialogResult
    /// </summary>
    /// <returns></returns>
    virtual public DialogTree GetDialog() {
        return null;
    }
    /// <summary>
    /// gets called by display dialog when player presses a button. The parameter is the choice-ID connected to the button.  
    /// </summary>
    /// <param name="i"></param>
    virtual public void SetDialogResult(int i) { }
}
