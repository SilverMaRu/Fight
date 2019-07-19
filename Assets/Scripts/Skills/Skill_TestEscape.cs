using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_TestEscape : SkillOnFrontStatus
{
    private Transform rootTrans;
    private Vector3 moveDirction = Vector3.zero;

    public Skill_TestEscape(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
        rootTrans = mySkillManager.transform;
    }

    public override bool MeetEnterCondition()
    {
        return base.MeetEnterCondition()
            && input.IsKeyDown(orderKeyCodeIdxs)
            ;
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        moveDirction = -rootTrans.right;
    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();
        rootTrans.position += moveDirction * Time.deltaTime * 2f;
    }
}
