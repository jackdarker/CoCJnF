using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Game
{
    public List<Player> players;
    public int currentPlayerIndex;
    public Player CurrentPlayer { get { return players[currentPlayerIndex]; } }
}