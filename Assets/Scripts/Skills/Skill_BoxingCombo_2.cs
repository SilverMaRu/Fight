using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BoxingCombo_2 : NormalSkill
{
    private int layerHurtOnly;
    private int layerAttackOnly;

    public Skill_BoxingCombo_2(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
        layerHurtOnly = LayerMask.NameToLayer("HurtOnly");
        layerAttackOnly = LayerMask.NameToLayer("AttackOnly");
    }

    public override bool MeetEnterCondition()
    {
        return base.MeetEnterCondition() && IsMyFrontSkill(mySkillManager.currentSkill) && input.IsKeyDown(orderKeyCodeIdxs);
    }

    public override void EnableInfluence(string influenceGOName)
    {
        GameObject attackGO = GetInfluencePartGameObjcet(influenceGOName);
        if (attackGO == null) return;
        int layerValue = attackGO.layer;
        layerValue = Mathf.Clamp(++layerValue, layerHurtOnly, layerAttackOnly);
        attackGO.layer = layerValue;
    }

    public override void DisableInfluence(string influenceGOName)
    {
        GameObject attackGO = GetInfluencePartGameObjcet(influenceGOName);
        if (attackGO == null) return;
        int layerValue = attackGO.layer;
        layerValue = Mathf.Clamp(--layerValue, layerHurtOnly, layerAttackOnly);
        attackGO.layer = layerValue;
    }
}
