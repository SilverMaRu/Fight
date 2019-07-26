using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillSpawInfo")]
public class SkillSpawInfo : ScriptableObject
{
    public int skillID;
    public string skillName;
    // 百分比消耗基础生命值
    public float useBaseHPMultiple;
    // 百分比消耗当前生命
    public float useCurrentHPMultiple;
    // 消耗当前生命值
    public int useHP;
    // 百分比消耗基础魔力值
    public float useBaseMPMultiple;
    // 百分比消耗当前魔力值
    public float useCurrentMPMultiple;
    // 消耗当前魔力值
    public float useMP;
    // 百分比消耗基础耐力值
    public float useBaseSPMultiple;
    // 百分比消耗当前耐力值
    public float useCurrentSPMultiple;
    // 消耗当前耐力值
    public int useSP;
    // 百分比修正攻击力
    public float attackReviseMultiple;
    // 修正攻击力
    public float attackRevise;
    // 百分比修正防御力
    public float defenseReviseMultiple;
    // 修正防御力
    public float defenseRevise;
    // 百分比修正速度
    public float speedReviseMultiple;
    // 修正速度
    public float speedRevise;
    public string frontStatusTypeName;
    // 技能指令
    public KeyCodeIndex[] orderKeyCodeIdxs = new KeyCodeIndex[1];
    // 受影响的部位的游戏对象名称
    public string[] influencedPartGameObjectNames = new string[0];
    public string[] frontSkillTypeNames = new string[0];
    public string[] deriveSkillTypeNames = new string[0];
}
