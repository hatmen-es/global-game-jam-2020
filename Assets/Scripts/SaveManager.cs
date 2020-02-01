using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    // Prevent non-singleton constructor use.
    protected SaveManager() { }

    public string playerName = "";
    public int playerScore = 0;

    private static string playerNameKey = "PLAYER_NAME";
    private static string playerScoreKey = "PLAYER_SCORE";


    public void Awake()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetString(playerNameKey, playerName);
        PlayerPrefs.SetInt(playerScoreKey, playerScore);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            playerName = PlayerPrefs.GetString(playerNameKey);
        }

        if (PlayerPrefs.HasKey(playerScoreKey))
        { 
            playerScore = PlayerPrefs.GetInt(playerScoreKey);
        }
    }
}
