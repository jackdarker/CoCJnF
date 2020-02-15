using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

public class nTrue : ICondition {

    private static nTrue Instance=null;
    public static nTrue GetInstance() {
        if (Instance==null) {
            Instance = new nTrue();
        }
        return Instance;
    }

    public nTrue() { }

    bool ICondition.Evaluate() {
        return true;
    }
    string ICondition.GetName() {
        return "nAlways";
    }
    string ICondition.GetText() {
        return "Always";
    }
}

