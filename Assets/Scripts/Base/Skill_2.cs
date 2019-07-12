using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SpawSkillInfo
{
    public int skillID;
    public string skillName;
    public int healthRevise;
    public int manaRevise;
    public int strengthRevise;
    public int attackRevise;
    public int defenseRevise;
    public int speedRevise;
    // 技能指令
    public int[] orderKeyCodeIdxs;
    // 带攻击判定的游戏对象名称
    public string[] hitDetectionGameObjectNames = new string[0];
    public string[] frontSkillTypeNames = new string[0];
    public string[] deriveSkillTypeNames = new string[0];
}

public class Skill_2
{
    private static Type[] skillTypes;
    public static Type[] SkillTypes
    {
        get
        {
            Type[] resultArray = null;
            if (skillTypes == null)
            {
                skillTypes = TypeHelper.GetSubclassesOf(typeof(Skill_2));
            }
            if(skillTypes != null)
            {
                resultArray = new Type[skillTypes.Length];
                skillTypes.CopyTo(resultArray, 0);
            }
            return resultArray;
        }
    }

    public SpawSkillInfo info;
    // 技能指令
    public KeyCodeIndex[] orderKeyCodeIdxs;
    //// 带攻击判定的游戏对象名称
    //public string[] hitDetectionGameObjectNames;
    // 前置技能
    public Skill_2[] frontSkills = new Skill_2[0];
    // 派生技能
    public Skill_2[] deriveSkills = new Skill_2[0];

    public GameObject rootGO;
    protected SkillManager mySkillManager;
    protected Animator anim;
    protected FightInput input;
    protected GameObject[] hitDetectionGOs = new GameObject[0];

    public Skill_2(GameObject rootGO, SkillManager skillManager, Animator anim)
    {
        this.rootGO = rootGO;
        mySkillManager = skillManager;
        this.anim = anim;
        input = mySkillManager.fightInput;
        //orderKeyCodeIdxs = IntToKeyCodeIndex(info.orderKeyCodeIdxs);
        //hitDetectionGOs = FindAttackGameObjects(info.hitDetectionGameObjectNames);
    }

    public virtual void EnterSkill()
    {
        if (mySkillManager != null && Equals(mySkillManager.currentSkill)) return;
        if (mySkillManager != null && mySkillManager.currentSkill_2 != null) mySkillManager.currentSkill_2.ExitSkill();
        if (mySkillManager != null) mySkillManager.currentSkill_2 = this;
        if (anim != null && !string.IsNullOrEmpty(info.skillName)) anim.SetBool(info.skillName, true);
    }

    public virtual void ExitSkill()
    {
        if (anim != null && !string.IsNullOrEmpty(info.skillName)) anim.SetBool(info.skillName, false);
        if (mySkillManager != null) mySkillManager.currentSkill_2 = null;
    }

    public virtual bool MeetEnterCondition()
    {
        return false;
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

    protected KeyCodeIndex[] IntToKeyCodeIndex(int[] intArray)
    {
        int length = intArray.Length;
        KeyCodeIndex[] resultArray = new KeyCodeIndex[length];
        for (int i = 0; i < length; i++)
        {
            resultArray[i] = (KeyCodeIndex)info.orderKeyCodeIdxs[i];
        }
        return resultArray;
    }

    protected GameObject[] FindAttackGameObjects(string[] attackGameObjectNames)
    {
        int length = attackGameObjectNames.Length;
        GameObject[] resultGOArray = new GameObject[length];
        Transform myTrans = rootGO.transform;
        for (int i = 0; i < length; i++)
        {
            resultGOArray[i] = Tool.RecursionFindGameObject(myTrans, attackGameObjectNames[i]);
        }
        return resultGOArray;
    }

    public void SetFrontSkills(Skill_2[] skillPool)
    {
        if (info.frontSkillTypeNames == null) return;
        List<Skill_2> frontSkillList = new List<Skill_2>(info.frontSkillTypeNames.Length);
        foreach(string tempTypeName in info.frontSkillTypeNames)
        {
            foreach(Skill_2 tempSkill in skillPool)
            {
                if (tempTypeName.Equals(tempSkill.GetType().Name))
                {
                    frontSkillList.Add(tempSkill);
                }
            }
        }
        frontSkills = frontSkillList.ToArray();
    }

    public void SetDeriveSkills(Skill_2[] skillPool)
    {
        if (info.deriveSkillTypeNames == null) return;
        List<Skill_2> deriveSkillList = new List<Skill_2>(info.deriveSkillTypeNames.Length);
        foreach (string tempTypeName in info.deriveSkillTypeNames)
        {
            foreach (Skill_2 tempSkill in skillPool)
            {
                if (tempTypeName.Equals(tempSkill.GetType().Name))
                {
                    deriveSkillList.Add(tempSkill);
                }
            }
        }
        deriveSkills = deriveSkillList.ToArray();
    }
}
