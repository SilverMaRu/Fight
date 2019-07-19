using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillSpawInfo")]
public class SkillSpawInfo : ScriptableObject
{
    public int skillID;
    public string skillName;
    public int healthRevise;
    public int manaRevise;
    public int strengthRevise;
    public int attackRevise;
    public int defenseRevise;
    public int speedRevise;
    public string frontStatusTypeName;
    // 技能指令
    public KeyCodeIndex[] orderKeyCodeIdxs = new KeyCodeIndex[1];
    // 受影响的部位的游戏对象名称
    public string[] influencedPartGameObjectNames = new string[0];
    public string[] frontSkillTypeNames = new string[0];
    public string[] deriveSkillTypeNames = new string[0];
}
