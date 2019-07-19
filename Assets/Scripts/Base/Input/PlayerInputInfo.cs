using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerInputInfo")]
[System.Serializable]
public class PlayerInputInfo : ScriptableObject
{
    public const string WithoutStreamingAssetsPath = "/Config/Input/";
    public const string ConfigFileNameSuffix = ".config";
    public const string PlayerInputInfoPath = "SriptableObjects/DefaultPlayerInputInfo/";
    public const string DefaultFileName = "DefaultInputInfo";

    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode baseSkillKey1 = KeyCode.J;
    public KeyCode baseSkillKey2 = KeyCode.K;
    public KeyCode baseSkillKey3 = KeyCode.L;
    public KeyCode baseSkillKey4 = KeyCode.U;
    public KeyCode baseSkillKey5 = KeyCode.I;
    public KeyCode baseSkillKey6 = KeyCode.O;

    public static void SavePlayerInputConfig(int savePlayerIndex, PlayerInputInfo saveInfo)
    {
        string directory = Application.streamingAssetsPath + WithoutStreamingAssetsPath;
        string defaultPlayerName = Player.DefaultPlayerName(savePlayerIndex);
        string fileName = defaultPlayerName + ConfigFileNameSuffix;
        string fullPath = directory + fileName;

        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.FileStream fs = System.IO.File.OpenWrite(fullPath);
        bf.Serialize(fs, saveInfo);
        fs.Flush();
        fs.Close();
    }

    public static PlayerInputInfo LoadPlayerInputConfig(int loadPlayerIndex)
    {
        PlayerInputInfo resultInfo = null;

        string directory = Application.streamingAssetsPath + WithoutStreamingAssetsPath;
        string defaultPlayerName = Player.DefaultPlayerName(loadPlayerIndex);
        string fileName = defaultPlayerName + ConfigFileNameSuffix;
        string fullPath = directory + fileName;

        if (System.IO.File.Exists(fullPath))
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(fullPath);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            resultInfo = (PlayerInputInfo)bf.Deserialize(fs);
            fs.Close();
        }
        else
        {
            resultInfo = Resources.Load<PlayerInputInfo>(PlayerInputInfoPath + defaultPlayerName + DefaultFileName);
        }
        return resultInfo;
    }
}
