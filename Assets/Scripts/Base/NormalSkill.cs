﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSkill : Skill
{
    public NormalSkill(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
    }

    public override bool MeetEnterCondition()
    {
        Status currentStatus = StatusManager.GetCurrentStatus(rootGO);
        return base.MeetEnterCondition()
            && (currentStatus == null || !currentStatus.GetType().IsSubclassOf(typeofROS))
            ;
    }
}
