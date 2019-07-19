using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private Player player01;
    // Start is called before the first frame update
    void Start()
    {
        player01 = new Player();

        Debug.Log("Player.PlayingPlayers.Length = " + Player.PlayingPlayers.Length);
        Debug.Log("Player.PlayingPlayers[0].playerName = " + Player.PlayingPlayers[0].playerName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
