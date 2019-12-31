using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTreeBuilder : MonoBehaviour
{
    private static DialogTreeBuilder singleton;
    public static DialogTreeBuilder getSingleton() {
        if (singleton == null)
            singleton = new DialogTreeBuilder();
        return singleton;
    }

    private DialogTreeBuilder() {

    }

    public DialogTree GetDialogTree(int[] NPC) {
        DialogTree _tree = new DialogTree();

        return _tree;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

}
