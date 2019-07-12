using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Sprinting : Status
{
    public float sprintingSpeed;

    private FightInput input;
    private Transform rootTrans;

    public Status_Sprinting(GameObject rootGO, StatusManager statusManager, Animator anim) : base(rootGO, statusManager, anim)
    {
        rootTrans = rootGO.transform;
        input = myStatusManager.fightInput;

        animBoolName = "Sprinting";
        sprintingSpeed = 4;
    }

    public override void EnterStatus()
    {
        base.EnterStatus();
    }

    public override void ExitStatus()
    {
        base.ExitStatus();
    }

    public override void Update()
    {
        base.Update();
        Vector3 newPosition = rootTrans.position;
        newPosition += rootTrans.right * sprintingSpeed * Time.deltaTime;
        if (!Tool.IsOutOfCameraX(newPosition.x))
        {
            rootTrans.position = newPosition;
        }
    }

    public override bool MeetEnterCondition()
    {
        return input.IsDoubleKeyDown(KeyCodeIndex.RightKeyCodeIdx) && rootTrans.right.x > 0
            || input.IsDoubleKeyDown(KeyCodeIndex.LeftKeyCodeIdx) && rootTrans.right.x < 0
            ;
    }

    public override bool MeetExitCondition()
    {
        return rootTrans.right.x > 0 && input.IsKeyUp(KeyCodeIndex.RightKeyCodeIdx)
            || rootTrans.right.x < 0 && input.IsKeyUp(KeyCodeIndex.LeftKeyCodeIdx)
            ;
    }
}
