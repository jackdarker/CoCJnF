using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;
using System;
public class Battle //Todo holds the data of the BattleWaves
{
    
    public class BattleGenerator {
        //generates a number of wave depending on location and difficulty
        public static Battle RandomizeBattle() {
            return new Battle(); //todo
        }
    }

    public Battle(){
        CurrWave = 0;
    }
    private int CurrWave;
    private Wave[] m_Waves;
    public Wave GetWave() {
        if(CurrWave> m_Waves.Length) return null;
        return m_Waves[CurrWave];
    }
    public void WaveDone()
    {
        CurrWave++;
    }
}