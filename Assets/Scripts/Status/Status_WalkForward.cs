﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_WalkForward : Status
{
    public float walkSpeed;

    private Transform rootTrans;
    private FightInput input;

    public Status_WalkForward(GameObject rootGO, StatusManager statusManager, Animator anim) : base(rootGO, statusManager, anim)
    {
        rootTrans = this.rootGO.transform;
        input = myStatusManager.fightInput;

        animBoolName = "Forward";
        walkSpeed = 2;
    }

    public override bool MeetEnterCondition()
    {
        return (rootTrans.right.x > 0 && input.IsKey(KeyCodeIndex.RightKeyCodeIdx))
            || (rootTrans.right.x < 0 && input.IsKey(KeyCodeIndex.LeftKeyCodeIdx))
            ;
    }

    public override bool MeetExitCondition()
    {
        return rootTrans.right.x > 0 && input.IsKeyUp(KeyCodeIndex.RightKeyCodeIdx)
            || rootTrans.right.x < 0 && input.IsKeyUp(KeyCodeIndex.LeftKeyCodeIdx)
            ;
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
        newPosition += rootTrans.right * walkSpeed * Time.deltaTime;
        if (!Tool.IsOutOfCameraX(newPosition.x))
        {
            rootTrans.position = newPosition;
        }
    }
}