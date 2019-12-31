using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSceneData : MonoBehaviour
{

    public DialogSceneData() {

    }

    virtual public int GetUId() {
        return 0;           //Todo force overriding this
    }

    virtual public void Setup() {

    }

    virtual public DialogTree GetDialog() {
        return null;
    }

}
