using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    public int skillID;
    public string skillName;
    public int healthRevise;
    public int manaRevise;
    public int strengthRevise;
    public int attackRevise;
    public int defenseRevise;
    public int speedRevise;
}

public class Skill : MonoBehaviour
{
    // 当前技能
    public static Skill currentSkill;

    public FightInput fightInput;
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
    public KeyCode[] orderKeyCodes;
    public string animTriggerName;
    public Animator anim;
    // 前置技能
    public Skill[] frontSkills;
    // 派生技能
    public Skill[] deriveSkills;

    private KeyCode[] sortOrderKeyCodes;

    private void OnEnable()
    {
        enabled = Contains(currentSkill, frontSkills);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(fightInput == null)
        {
            fightInput = GetComponent<FightInput>();
        }
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        enabled = Contains(currentSkill, frontSkills);
        sortOrderKeyCodes = Tool.Sort(orderKeyCodes);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (anim == null) return;
        // 如果输入指令与设定指令相同
        if(fightInput.IsKeyDown(sortOrderKeyCodes))
        {
            EnterSkill();
        }
    }

    public virtual void EnableDeriveSkill()
    {
        if(deriveSkills != null)
        {
            foreach(Skill tempSkill in deriveSkills)
            {
                tempSkill.enabled = true;
            }
        }
    }

    public virtual void DisableDeriveSkill()
    {
        if (deriveSkills != null)
        {
            foreach (Skill tempSkill in deriveSkills)
            {
                tempSkill.enabled = false;
            }
        }
    }

    public virtual void EnterSkill()
    {
        anim.SetTrigger(animTriggerName);
        currentSkill = this;
    }

    public virtual void ExitSkill()
    {
        Debug.Log("Do EndSkill");
        currentSkill = null;
    }

    public bool Contains(Skill other)
    {
        //bool contains = other != null && skilInfo.skillID == other.skilInfo.skillID;
        bool contains = other != null && skillID == other.skillID;
        return contains;
    }

    public bool Contains(Skill[] others)
    {
        bool contains = others != null;
        foreach(Skill tempSkill in others)
        {
            contains = contains && Contains(tempSkill);
            if (!contains)
            {
                break;
            }
        }
        return contains;
    }

    public static bool Contains(Skill skill0, Skill skill1)
    {
        return skill0 == null && skill1 == null || skill0.Contains(skill1);
    }
    
    public static bool Contains(Skill skill, Skill[] others)
    {
        return skill.Contains(others);
    }
}
