using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Idle : Status
{
    public Status_Idle(GameObject rootGO, StatusManager statusManager, Animator anim) : base(rootGO, statusManager, anim)
    {
        statusName = "Idle";
    }

    public override bool MeetEnterCondition()
    {
        return base.MeetEnterCondition()
            && !input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx1)
            && !input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx2)
            && !input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx3)
            && !input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx4)
            && !input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx5)
            && !input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx6)
            && !input.IsKeyDown(KeyCodeIndex.UpKeyCodeIdx)
            && !input.IsKeyDown(KeyCodeIndex.DownKeyCodeIdx)
            && !input.IsKeyDown(KeyCodeIndex.LeftKeyCodeIdx)
            && !input.IsKeyDown(KeyCodeIndex.RightKeyCodeIdx)
            ;
    }

    public override bool MeetExitCondition()
    {
        return base.MeetExitCondition()
            || input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx1)
            || input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx2)
            || input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx3)
            || input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx4)
            || input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx5)
            || input.IsKeyDown(KeyCodeIndex.BaseSkillKeyCodeIdx6)
            || input.IsKeyDown(KeyCodeIndex.UpKeyCodeIdx)
            || input.IsKeyDown(KeyCodeIndex.DownKeyCodeIdx)
            || input.IsKeyDown(KeyCodeIndex.LeftKeyCodeIdx)
            || input.IsKeyDown(KeyCodeIndex.RightKeyCodeIdx)
        ;
    }
}
