using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[Serializable]
public class DataController
{
    #region Fields
    const string saveDataKey = "GameSaveData";
    public static readonly DataController instance = new DataController();
    public DatabaseController pokemonDatabase;
    public Game game;
   /* public Board board;*/
    public Battle battle;
    #endregion

    #region Public
    public void Load(Action complete)
    {
        pokemonDatabase = new DatabaseController("Pokemon.db");
        pokemonDatabase.Load(SQLiteOpenFlags.ReadOnly, delegate (DatabaseController obj) {
            if (complete != null)
                complete();
        });
    }

    public void SaveGame()
    {
        var json = JsonUtility.ToJson(game);
        PlayerPrefs.SetString(saveDataKey, json);
    }

    public bool HasSavedGame()
    {
        return PlayerPrefs.HasKey(saveDataKey);
    }

    public void LoadGame()
    {
        var json = PlayerPrefs.GetString(saveDataKey);
        game = GameFactory.Create(json);
    }

    public void ClearSavedGame()
    {
        PlayerPrefs.DeleteKey(saveDataKey);
    }
    #endregion

    #region Constructor / Destructor
    private DataController()
    {

    }

    ~DataController()
    {
        pokemonDatabase.connection.Close();
    }
    #endregion
}