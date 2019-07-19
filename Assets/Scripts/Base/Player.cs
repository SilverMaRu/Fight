using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private static List<Player> playingPlayerList = new List<Player>();
    public static Player[] PlayingPlayers { get { return playingPlayerList.ToArray(); } }

    public int playerIndex;
    public string playerName;
    public PlayerInputInfo playerInputInfo;

    public Player()
    {
        playerIndex = playingPlayerList.Count;
        playerName = DefaultPlayerName(playerIndex);
        playerInputInfo = PlayerInputInfo.LoadPlayerInputConfig(playerIndex);
        playingPlayerList.Add(this);
    }

    public Player(string playerName)
    {
        playerIndex = playingPlayerList.Count;
        this.playerName = playerName;
        playerInputInfo = PlayerInputInfo.LoadPlayerInputConfig(playerIndex);
        playingPlayerList.Add(this);
    }

    public static string DefaultPlayerName(int playerIndex)
    {
        string resultName = string.Empty;
        int num = playerIndex + 1;
        resultName = "Player_" + num.ToString().PadLeft(2, '0');
        return resultName;
    }
}
