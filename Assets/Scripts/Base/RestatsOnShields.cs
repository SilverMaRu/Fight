using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestatsOnShields : Status
{
    protected float durationTime = 1;
    protected float enterTime = 0;
    protected bool exit = false;
    protected System.Type typeOfSkillEsc = typeof(Skill_TestEscape);

    public RestatsOnShields(GameObject rootGO, StatusManager statusManager, Animator anim) : base(rootGO, statusManager, anim)
    {
        Part.BeHurtEvent += OnBeHurt;
        Skill.EnterSkillEvent += OnEnterSkill;
    }

    public override bool MeetEnterCondition()
    {
        return false;
    }

    public override void EnterStatus()
    {
        base.EnterStatus();
        enterTime = Time.time;
    }

    public override bool MeetExitCondition()
    {
        return Time.time - enterTime > durationTime;
    }

    public virtual void SetDurationTime(float durationTime)
    {
        this.durationTime = Mathf.Abs(durationTime);
    }

    protected virtual void OnBeHurt(GameObject rootGO, Part beHurtPart)
    {
        if (rootGO.Equals(this.rootGO))
        {
            EnterStatus();
        }
    }

    protected virtual void OnEnterSkill(GameObject rootGO, Skill useSkill)
    {
        if (rootGO.Equals(this.rootGO) && useSkill.GetType().IsSubclassOf(typeOfSkillEsc))
        {
            ExitStatus();
        }
    }
}
