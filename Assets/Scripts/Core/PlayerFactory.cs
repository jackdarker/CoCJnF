using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerFactory
{
    public static Player Create() 
    {
        var player = new Player();
        return player;
    }
}