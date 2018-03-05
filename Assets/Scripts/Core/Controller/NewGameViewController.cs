using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ECS;

public class NewGameViewController : BaseViewController
{

    public Action didAbort;
    public Action didComplete;
   /* ??public Action<int> didComplete;

    public void SetPlayerCount(int count)
    {
        if (didComplete != null)
            didComplete(count);
    }*/

    [SerializeField] Text playerLabel;
    [SerializeField] InputField nameInput;
    [SerializeField] Image[] pokemonAvatars;
    Entity buddy;
    Entity[] pokemon;
    int playerIndex = 0;

    void Awake()
    {
        pokemon = new Entity[3];
        var connection = DataController.instance.pokemonDatabase.connection;
        pokemon[0] = connection.Table<Entity>().Where(x => x.id == 1).FirstOrDefault();
        pokemon[1] = connection.Table<Entity>().Where(x => x.id == 4).FirstOrDefault();
        pokemon[2] = connection.Table<Entity>().Where(x => x.id == 7).FirstOrDefault();
    }

    void OnEnable()
    {
        SetPlayerIndex(0);
    }

    public void OnContinueButton()
    {
        SavePlayer();
        SetPlayerIndex(playerIndex + 1);
    }

    public void OnBackButton()
    {
        SetPlayerIndex(playerIndex - 1);
    }

    public void SelectPokemon(int index)
    {
     /*   buddy = pokemon[index];
        for (int i = 0; i < pokemon.Length; ++i)
        {
            Poses pose = buddy == pokemon[i] ? Poses.Front : Poses.Back;
            pokemonAvatars[i].sprite = PokemonSystem.GetAvatar(pokemon[i], Genders.Male, pose);
        }*/
    }

    void SetPlayerIndex(int index)
    {
        if (index < 0)
        {
            if (didAbort != null)
                didAbort();
        }
        else if (index >= game.players.Count)
        {
            if (didComplete != null)
                didComplete();
        }
        else
        {
            playerIndex = index;
            LoadPlayer();
        }
    }

    public void LoadPlayer()
    {
        playerLabel.text = string.Format("Player {0}", (playerIndex + 1));
        nameInput.text = string.Empty;
        int random = UnityEngine.Random.Range(0, 3);
        SelectPokemon(random);
    }

    void SavePlayer()
    {
        var player = game.players[playerIndex];
        player.nickName = string.IsNullOrEmpty(nameInput.text) ? playerLabel.text : nameInput.text;
       /* player.pokemon.Clear();
        player.pokemon.Add(PokemonFactory.Create(buddy, 4));*/
    }
}