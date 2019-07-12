using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{    
    //public SkillInfo skilInfo;
    public int skillID;
    public string skillName;
    public int healthRevise;
    public int manaRevise;
    public int strengthRevise;
    public int attackRevise;
    public int defenseRevise;
    public int speedRevise;
    // 技能指令
    public KeyCodeIndex[] orderKeyCodeIdxs;
    public string animTriggerName;
    // 带攻击判定的游戏对象名称
    public string[] hitDetectionGameObjectNames;
    public Animator anim;
    // 前置技能
    public Skill[] frontSkills;
    // 派生技能
    public Skill[] deriveSkills;

    private GameObject[] hitDetectionGOs;

    protected virtual void Awake()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        hitDetectionGOs = FindAttackGameObjects(hitDetectionGameObjectNames);
    }

    public virtual void EnterSkill()
    {
        if (anim != null && !string.IsNullOrEmpty(animTriggerName)) anim.SetTrigger(animTriggerName);
    }

    public virtual void ExitSkill()
    {
        //enabled = false;
    }

    public GameObject GetAttackGameObjcet(string gameObjectName)
    {
        GameObject resultGO = null;
        foreach (GameObject tempGO in hitDetectionGOs)
        {
            if (tempGO.name.Equals(gameObjectName))
            {
                resultGO = tempGO;
                break;
            }
        }
        return resultGO;
    }

    protected GameObject[] FindAttackGameObjects(string[] attackGameObjectNames)
    {
        int length = attackGameObjectNames.Length;
        GameObject[] resultGOArray = new GameObject[length];
        Transform myTrans = transform;
        for(int i = 0; i < length; i++)
        {
            resultGOArray[i] = Tool.RecursionFindGameObject(myTrans, attackGameObjectNames[i]);
        }
        return resultGOArray;
    }
}
