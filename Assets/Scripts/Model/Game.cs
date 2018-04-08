using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Game
{
    private double m_Time;  //1.5 = 1:30;
    private int m_Days;
    public EventHandler EvtClockChange;
    public string GetTimeAsString() { return m_Time.ToString(); }  //Todo format
    public string GetDaysAsString() { return m_Days.ToString(); }  //Todo format
    public void AddTimeToClock(double Hours) {
        double _time = m_Time + Hours;
        int _Days = 0;
        while (_time > 24) {
            _Days++;
            _time -= 24;
        }
        m_Days += _Days;
        m_Time += _time;
        if (EvtClockChange != null)
            EvtClockChange(this, EventArgs.Empty);
    }
    
    

    public List<Player> players;
    public int currentPlayerIndex;
    public Player CurrentPlayer { get { return players[currentPlayerIndex]; } }
}