using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

public class nAnd : ICondition {
    ICondition[] Conditions = new ICondition[0];

    public nAnd() { }
    public nAnd(ICondition[] Cond) {
        Conditions = Cond;
    }
    bool ICondition.Evaluate() {
        bool ret = true;
        foreach(ICondition cond in Conditions) {
            ret = ret && (cond.Evaluate()) ;
        }
        return ret;
    }
    string ICondition.GetName() {
        return "nAnd";
    }
    string ICondition.GetText() {
        StringBuilder strb = new StringBuilder();
        
        foreach (ICondition cond in Conditions) {
            strb.Append("and ");
            strb.AppendLine(cond.GetName());
        }
        return strb.ToString();
    }
}

