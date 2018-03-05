using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFactory
{
    public static Game Create(int playerCount)
    {
        Game game = new Game();
        game.players = new List<Player>(playerCount);
        for (int i = 0; i < playerCount; ++i)
        {
            var player = PlayerFactory.Create();
            game.players.Add(player);
        }
        return game;
    }

    public static Game Create(string json)
    {
        Game game = JsonUtility.FromJson<Game>(json);

        return game;
    }
}