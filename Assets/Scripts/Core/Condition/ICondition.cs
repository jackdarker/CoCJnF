using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition 
{
    string GetName();
    bool Evaluate();
}
