using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BoxingCombo_1 : NormalSkill
{
    private int layerHurtOnly;
    private int layerAttackOnly;

    public Skill_BoxingCombo_1(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
        layerHurtOnly = LayerMask.NameToLayer("HurtOnly");
        layerAttackOnly = LayerMask.NameToLayer("AttackOnly");
    }

    public override bool MeetEnterCondition()
    {
        return base.MeetEnterCondition() && input.IsKeyDown(orderKeyCodeIdxs);
    }

    public override void EnableInfluence(string influenceGOName)
    {
        GameObject attackGO = GetInfluencePartGameObjcet(influenceGOName);
        if (attackGO != null)
        {
            Part attackPart = attackGO.GetComponent<Part>();
            if (attackPart != null)
            {
                attackPart.ResetHitGameObjectList();
                attackPart.UpgradeLayer();
            }
        }
    }

    public override void DisableInfluence(string influenceGOName)
    {
        GameObject attackGO = GetInfluencePartGameObjcet(influenceGOName);
        if (attackGO != null)
        {
            Part attackPart = attackGO.GetComponent<Part>();
            if (attackPart != null)
            {
                attackPart.DowngradeLayer();
            }
        }
    }
}
