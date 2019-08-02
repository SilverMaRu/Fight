using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOnFrontStatus : Skill
{
    protected System.Type frontStatusType;

    public SkillOnFrontStatus(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
        MatchFrontStatusType(Status.StatusTypes);
    }

    public override bool MeetEnterCondition()
    {
        return base.MeetEnterCondition()
            && IsMyFrontStatus(StatusManager.GetCurrentStatus(rootGO))
            ;
    }

    public void MatchFrontStatusType(System.Type[] typePool)
    {
        frontStatusType = TypeHelper.GetType(typePool, info.frontStatusTypeName);
    }

    public bool IsMyFrontStatus(Status status)
    {
        return status != null
            && status.GetType().IsSubclassOf(frontStatusType);
    }
}
