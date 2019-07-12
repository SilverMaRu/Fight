using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BoxingCombo_2 : Skill_2
{
    public Skill_BoxingCombo_2(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
        info = new SpawSkillInfo();
        info.orderKeyCodeIdxs = new int[] { (int)KeyCodeIndex.BaseSkillKeyCodeIdx1 };
        info.deriveSkillTypeNames = new string[] { typeof(Skill_BoxingCombo_1).Name };
        info.hitDetectionGameObjectNames = new string[] { "LeftLowerArm" };

        orderKeyCodeIdxs = IntToKeyCodeIndex(info.orderKeyCodeIdxs);
        hitDetectionGOs = FindAttackGameObjects(info.hitDetectionGameObjectNames);
    }

    public override bool MeetEnterCondition()
    {
        return input.IsKeyDown(orderKeyCodeIdxs);
    }
}
