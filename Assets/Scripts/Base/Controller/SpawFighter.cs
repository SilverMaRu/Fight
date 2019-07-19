using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawFighter : MonoBehaviour
{
    private static Dictionary<Player, FighterSpawInfo> playerSelectedFightInfoPairs = new Dictionary<Player, FighterSpawInfo>();
    private GameObject fighterPrefab;

    public static GameObject[] fighterGOIns;

    private void Awake()
    {
        TestSelet();
        fighterPrefab = Resources.Load<GameObject>("Prefabs/Fighter");
        fighterGOIns = SpawFighters();
    }

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.
    }

    public GameObject[] SpawFighters()
    {
        int dicCount = playerSelectedFightInfoPairs.Count;
        List<GameObject> resultGOList = new List<GameObject>(dicCount);
        Player[] keys = new Player[dicCount];
        playerSelectedFightInfoPairs.Keys.CopyTo(keys, 0);
        FighterSpawInfo[] values = new FighterSpawInfo[dicCount];
        playerSelectedFightInfoPairs.Values.CopyTo(values, 0);

        for (int i = 0; i < dicCount; i++)
        {
            Player thisPlayer = keys[i];
            FighterSpawInfo thisFighterInfo = values[i];
            GameObject tempFighterGO = Instantiate(fighterPrefab);
            tempFighterGO.name = thisFighterInfo.fighterName;

            FightInput input = tempFighterGO.GetComponent<FightInput>();
            input.inputInfo = thisPlayer.playerInputInfo;

            SkillManager tempFighterSkillManager = tempFighterGO.GetComponent<SkillManager>();
            tempFighterSkillManager.testSkillTypes = thisFighterInfo.skillTypeNames;
            resultGOList.Add(tempFighterGO);
        }

        return resultGOList.ToArray();
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
}
