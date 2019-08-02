using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSkill : NormalSkill
{
    // 是否永久有效
    protected bool unfailing = false;
    // 持续时间(s)
    protected float duration = 60;
    protected float enterTime = 0;
    protected virtual float stayTime
    {
        get
        {
            return Time.time - enterTime;
        }
    }

    public StatusSkill(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        enterTime = Time.time;
        // 判断是否已经使用该状态技能, 避免叠加
        if (!mySkillManager.stayStatusSkillList.Contains(this))
        {
            EnterStatusSkill();
            if (!unfailing) mySkillManager.StartCoroutine(TimeUpExit());
        }
    }

    protected virtual void EnterStatusSkill()
    {
        mySkillManager.stayStatusSkillList.Add(this);
    }

    protected virtual bool MeetExitCondition()
    {
        bool result = false;
        if (stayTime > duration)
        {
            result = true;
        }
        return result;
    }

    protected virtual void ExitStatusSkill()
    {
        mySkillManager.stayStatusSkillList.Remove(this);
    }

    protected virtual IEnumerator TimeUpExit()
    {
        while (!MeetExitCondition())
        {
            yield return null;
        }
        ExitStatusSkill();
    }
}
