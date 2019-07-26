using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FighterAttributes
{
    // 生命值
    public float baseHP = 100;
    // 生命值恢复量 (秒/点)
    public float baseRecoverHP = 0;
    // 魔力值
    public int baseMP = 100;
    // 魔力值恢复量 (秒/点)
    public float baseRecoverMP = 1;
    // 耐力
    public float baseSP = 100;
    // 耐力值恢复量 (秒/点)
    public float baseRecoverSP = 25;
    // 攻击力
    public float baseAttack = 100;
    // 防御力
    public float baseDefense = 100;
    // 速度
    public float baseSpeed = 100;
}
