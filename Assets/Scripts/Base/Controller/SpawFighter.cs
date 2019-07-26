using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawFighter : MonoBehaviour
{
    private static Dictionary<Player, FighterSpawInfo> playerSelectedFightInfoPairs = new Dictionary<Player, FighterSpawInfo>();
    private static Dictionary<Player, GameObject> playerFighterInsPairs = new Dictionary<Player, GameObject>();
    private GameObject fighterPrefab;

    private void Awake()
    {
        TestSelet();
        fighterPrefab = Resources.Load<GameObject>("Prefabs/Fighter");
        SpawFighters();
    }

    //private void OnApplicationQuit()
    //{
    //    Debug.Log("Do OnApplicationQuit");
    //    int length = playerSelectedFightInfoPairs.Count;
    //    Player[] players = new Player[length];
    //    playerSelectedFightInfoPairs.Keys.CopyTo(players, 0);
    //    foreach (Player tempPlayer in players)
    //    {
    //        //PlayerInputInfo.SavePlayerInputConfig(tempPlayer);
    //        Player.SavePlayerInputConfig(tempPlayer);
    //    }
    //}

    public void SpawFighters()
    {
        int dicCount = playerSelectedFightInfoPairs.Count;
        Player[] keys = new Player[dicCount];
        playerSelectedFightInfoPairs.Keys.CopyTo(keys, 0);
        FighterSpawInfo[] values = new FighterSpawInfo[dicCount];
        playerSelectedFightInfoPairs.Values.CopyTo(values, 0);

        for (int i = 0; i < dicCount; i++)
        {
            Player thisPlayer = keys[i];
            FighterSpawInfo thisFighterInfo = values[i];
            Vector3 position = Vector3.right * (-3 + 6 * i);
            GameObject tempFighterGO = Instantiate(fighterPrefab, position, Quaternion.identity);
            tempFighterGO.name = thisFighterInfo.fighterName;

            FightInput input = tempFighterGO.GetComponent<FightInput>();
            input.inputInfo = thisPlayer.playerInputInfo;

            SkillManager tempFighterSkillManager = tempFighterGO.GetComponent<SkillManager>();
            tempFighterSkillManager.testSkillTypes = thisFighterInfo.skillTypeNames;

            FighterAttributesManager tempFighterAttrManager = tempFighterGO.GetComponent<FighterAttributesManager>();
            tempFighterAttrManager.fighterAttr = thisFighterInfo.fighterAttr;

            playerFighterInsPairs.Add(thisPlayer, tempFighterGO);
        }
    }

    public static void SelectFighter(Player player, FighterSpawInfo selectedFighterInfo)
    {
        playerSelectedFightInfoPairs.Add(player, selectedFighterInfo);
    }

    private void TestSelet()
    {
        Player[] players = new Player[] { new Player(), new Player() };
        FighterSpawInfo[] fighterSpawInfos = Resources.LoadAll<FighterSpawInfo>("SriptableObjects/SpawFighterInfo");
        int lenght = Mathf.Min(players.Length, fighterSpawInfos.Length);
        for (int i = 0; i < lenght; i++)
        {
            SelectFighter(players[i], fighterSpawInfos[i]);
        }
    }

    public static GameObject GetFighterIns(Player player)
    {
        GameObject result = null;
        playerFighterInsPairs.TryGetValue(player, out result);
        return result;
    }
}
