using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Skill
{
    public delegate void EnterSkillHandler(GameObject useGO, Skill useSkill);
    public delegate void ExitSkillHandler(GameObject useGO, Skill useSkill);
    public static event EnterSkillHandler EnterSkillEvent;
    public static event ExitSkillHandler ExitSkillEvent;

    private static Type[] skillTypes;
    public static Type[] SkillTypes
    {
        get
        {
            Type[] resultArray = null;
            if (skillTypes == null)
            {
                skillTypes = TypeHelper.GetSubclassesOf(typeof(Skill));
            }
            if(skillTypes != null)
            {
                resultArray = new Type[skillTypes.Length];
                skillTypes.CopyTo(resultArray, 0);
            }
            return resultArray;
        }
    }

    public SkillSpawInfo info;
    // 生命值消耗量
    public float HPConsumption
    {
        get
        {
            return attrManager.fighterAttr.baseHP * info.useBaseHPMultiple + attrManager.currentHP * info.useCurrentHPMultiple + info.useHP;
        }
    }
    // 魔力值消耗量
    public float MPConsumption
    {
        get
        {
            return attrManager.fighterAttr.baseMP * info.useBaseMPMultiple + attrManager.currentMP * info.useCurrentMPMultiple + info.useMP;
        }
    }
    // 耐力值消耗量
    public float SPConsumption
    {
        get
        {
            return attrManager.fighterAttr.baseSP * info.useBaseSPMultiple + attrManager.currentSP * info.useCurrentSPMultiple + info.useSP;
        }
    }
    // 技能指令
    public KeyCodeIndex[] orderKeyCodeIdxs;
    // 前置技能
    public Skill[] frontSkills = new Skill[0];
    // 派生技能
    public Skill[] deriveSkills = new Skill[0];

    public bool onEnable = true;
    public GameObject rootGO;
    protected SkillManager mySkillManager;
    protected Animator anim;
    protected FightInput input;
    protected FighterAttributesManager attrManager;
    protected GameObject[] influencePartGOs = new GameObject[0];
    protected Type typeofROS = typeof(RestatsOnShields);

    public Skill(GameObject rootGO, SkillManager skillManager, Animator anim)
    {
        this.rootGO = rootGO;
        mySkillManager = skillManager;
        this.anim = anim;
        input = mySkillManager.fightInput;
        attrManager = this.rootGO.GetComponent<FighterAttributesManager>();

        info = LoadSpawSkillInfo();
        if(info != null)
        {
            orderKeyCodeIdxs = info.orderKeyCodeIdxs;
            influencePartGOs = FindGameObjects(info.influencedPartGameObjectNames);
        }
    }

    private SkillSpawInfo LoadSpawSkillInfo()
    {
        string name = GetType().Name + "_SpawInfo";
        SkillSpawInfo resultInfo = Resources.Load<SkillSpawInfo>("SriptableObjects/SpawSkillInfo/" + name);
        return resultInfo;
    }

    public virtual bool MeetEnterCondition()
    {
        return onEnable
            && attrManager.currentHP > HPConsumption
            && attrManager.currentMP >= MPConsumption
            && attrManager.currentSP >= SPConsumption
            ;
    }

    public virtual void EnterSkill()
    {
        if (mySkillManager == null) return;
        if (Equals(mySkillManager.currentSkill)) return;
        if (mySkillManager.currentSkill != null) mySkillManager.currentSkill.ExitSkill();
        mySkillManager.listeningInput = false;
        mySkillManager.currentSkill = this;
        if (anim != null && !string.IsNullOrEmpty(info.skillName)) anim.SetTrigger(info.skillName);
        EnterSkillEvent?.Invoke(rootGO, this);
    }

    public virtual void UpdateSkill()
    {
    }

    public virtual void ExitSkill()
    {
        if (mySkillManager != null) mySkillManager.currentSkill = null;
        mySkillManager.listeningInput = true;
        ExitSkillEvent?.Invoke(rootGO, this);
    }

    public GameObject GetInfluencePartGameObjcet(string gameObjectName)
    {
        GameObject resultGO = null;
        foreach (GameObject tempGO in influencePartGOs)
        {
            if (tempGO.name.Equals(gameObjectName))
            {
                resultGO = tempGO;
                break;
            }
        }
        return resultGO;
    }

    protected GameObject[] FindGameObjects(string[] attackGameObjectNames)
    {
        int length = attackGameObjectNames.Length;
        GameObject[] resultGOArray = new GameObject[length];
        Transform myTrans = rootGO.transform;
        for (int i = 0; i < length; i++)
        {
            resultGOArray[i] = Tool.FindGameObject(myTrans, attackGameObjectNames[i]);
        }
        return resultGOArray;
    }

    public void MatchFrontSkills(Skill[] skillPool)
    {
        if (info.frontSkillTypeNames == null) return;
        List<Skill> frontSkillList = new List<Skill>(info.frontSkillTypeNames.Length);
        foreach(string tempTypeName in info.frontSkillTypeNames)
        {
            if (string.IsNullOrEmpty(tempTypeName))
            {
                frontSkillList.Add(null);
            }
            else
            {
                foreach(Skill tempSkill in skillPool)
                {
                    if (tempTypeName.Equals(tempSkill.GetType().Name))
                    {
                        frontSkillList.Add(tempSkill);
                    }
                }
            }
        }
        frontSkills = frontSkillList.ToArray();
    }

    public void MatchDeriveSkills(Skill[] skillPool)
    {
        if (info.deriveSkillTypeNames == null) return;
        List<Skill> deriveSkillList = new List<Skill>(info.deriveSkillTypeNames.Length);
        foreach (string tempTypeName in info.deriveSkillTypeNames)
        {
            foreach (Skill tempSkill in skillPool)
            {
                if (tempTypeName.Equals(tempSkill.GetType().Name))
                {
                    deriveSkillList.Add(tempSkill);
                }
            }
        }
        deriveSkills = deriveSkillList.ToArray();
    }

    public bool IsMyFrontSkill(Skill skill)
    {
        bool isMyFrontSkill = false;
        foreach(Skill frontSkillItem in frontSkills)
        {
            isMyFrontSkill = isMyFrontSkill || skill == null && frontSkillItem == null || skill != null && skill.GetType().Name.Equals(frontSkillItem.GetType().Name);
        }
        return isMyFrontSkill;
    }

    public virtual void EnableInfluence(string influenceGOName) { }

    public virtual void DisableInfluence(string influenceGOName) { }
}
