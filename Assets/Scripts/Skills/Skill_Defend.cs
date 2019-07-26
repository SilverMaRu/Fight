using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Defend : NormalSkill
{
    public Skill_Defend(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
    }

    public override bool MeetEnterCondition()
    {
        return base.MeetEnterCondition()
            && input.IsKey(orderKeyCodeIdxs)
            ;
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        foreach (string partName in info.influencedPartGameObjectNames)
        {
            EnableInfluence(partName);
        }
    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();
        if (!MeetEnterCondition())
        {
            ExitSkill();
        }
    }

    public override void ExitSkill()
    {
        base.ExitSkill();
        foreach (string partName in info.influencedPartGameObjectNames)
        {
            DisableInfluence(partName);
        }
    }

    public override void EnableInfluence(string influenceGOName)
    {
        GameObject blockGO = GetInfluencePartGameObjcet(influenceGOName);
        //if(blockGO != null)
        //{
        //    Part blockPart = blockGO.GetComponent<Part>();
        //    if(blockPart != null)
        //    {
        //        blockPart.block = true;
        //    }
        //}
        Part blockPart = blockGO.GetComponent<Part>();
        blockPart.isDefend = true;
    }

    public override void DisableInfluence(string influenceGOName)
    {
        GameObject blockGO = GetInfluencePartGameObjcet(influenceGOName);
        //if (blockGO != null)
        //{
        //    Part blockPart = blockGO.GetComponent<Part>();
        //    if (blockPart != null)
        //    {
        //        blockPart.block = false;
        //    }
        //}
        Part blockPart = blockGO.GetComponent<Part>();
        blockPart.isDefend = false;
    }
}
