using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

public class nAny : ICondition {
    ICondition[] Conditions = new ICondition[0];

    public nAny() { }
    public nAny(ICondition[] Cond) {
        Conditions = Cond;
    }
    bool ICondition.Evaluate() {
        foreach(ICondition cond in Conditions) {
            if (cond.Evaluate()) return true;
        }
        return false;
    }
    string ICondition.GetName() {
        return "nAny";
    }
    string ICondition.GetText() {
        StringBuilder strb = new StringBuilder();
        
        foreach (ICondition cond in Conditions) {
            strb.Append("or ");
            strb.AppendLine(cond.GetName());
        }
        return strb.ToString();
    }
}

