using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public const string WithoutStreamingAssetsPath = "/Config/Input/";
    public const string ConfigFileNameSuffix = ".config";
    public const string PlayerInputInfoPath = "SriptableObjects/DefaultPlayerInputInfo/";
    public const string DefaultFileName = "DefaultInputInfo";

    private static List<Player> playingPlayerList = new List<Player>();
    public static Player[] PlayingPlayers { get { return playingPlayerList.ToArray(); } }

    public int playerIndex;
    public string playerName;
    public PlayerInputInfo playerInputInfo;

    public Player()
    {
        playerIndex = playingPlayerList.Count;
        playerName = DefaultPlayerName(playerIndex);
        playerInputInfo = LoadPlayerInputConfig(playerIndex);
        playingPlayerList.Add(this);
    }

    public Player(string playerName)
    {
        playerIndex = playingPlayerList.Count;
        this.playerName = playerName;
        playerInputInfo = LoadPlayerInputConfig(playerIndex);
        playingPlayerList.Add(this);
    }

    public static string DefaultPlayerName(int playerIndex)
    {
        string resultName = string.Empty;
        int num = playerIndex + 1;
        resultName = "Player_" + num.ToString().PadLeft(2, '0');
        return resultName;
    }

    public static void SavePlayerInputConfig(Player savePlayer)
    {
        string directory = Application.streamingAssetsPath + WithoutStreamingAssetsPath;
        string defaultPlayerName = DefaultPlayerName(savePlayer.playerIndex);
        string fileName = defaultPlayerName + ConfigFileNameSuffix;
        string fullPath = directory + fileName;
        
        string saveData = PlayerInputInfo.ToSaveData(savePlayer.playerInputInfo);
        System.IO.File.WriteAllText(fullPath, saveData);
    }

    public static PlayerInputInfo LoadPlayerInputConfig(int loadPlayerIndex)
    {
        PlayerInputInfo resultInfo = null;

        string directory = Application.streamingAssetsPath + WithoutStreamingAssetsPath;
        string defaultPlayerName = DefaultPlayerName(loadPlayerIndex);
        string fileName = defaultPlayerName + ConfigFileNameSuffix;
        string fullPath = directory + fileName;

        if (System.IO.File.Exists(fullPath))
        {
            string saveData = System.IO.File.ReadAllText(fullPath);
            resultInfo = PlayerInputInfo.FromSaveData<PlayerInputInfo>(saveData);
        }
        else
        {
            resultInfo = Resources.Load<PlayerInputInfo>(PlayerInputInfoPath + defaultPlayerName + DefaultFileName);
        }

        return resultInfo;
    }

    public static Player GetPlayerByIndex(int playerIndex)
    {
        Player result = null;
        foreach(Player tempPlayer in playingPlayerList)
        {
            if(tempPlayer.playerIndex == playerIndex)
            {
                result = tempPlayer;
                break;
            }
        }
        return result;
    }
}
