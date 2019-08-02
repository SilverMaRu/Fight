using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Sprinting : Status
{
    public float sprintingSpeed
    {
        get
        {
            float result = 0;
            result = attrManager.speed * 2 * localTimeScale.localTimeScaleRatio;
            return result;
        }
    }

    private Transform rootTrans;
    private Collider2D coll;
    private int layerSize = -1;
    private bool queriesStartInCollidersMark = true;
    private FighterAttributesManager attrManager;
    private TimeScale localTimeScale;

    public Status_Sprinting(GameObject rootGO, StatusManager statusManager, Animator anim) : base(rootGO, statusManager, anim)
    {
        rootTrans = rootGO.transform;
        coll = this.rootGO.GetComponent<Collider2D>();

        layerSize = LayerMask.NameToLayer("Size");

        statusName = "Sprinting";
        attrManager = rootGO.GetComponent<FighterAttributesManager>();
        localTimeScale = rootGO.GetComponent<TimeScale>();
    }

    public override void EnterStatus()
    {
        base.EnterStatus();
        queriesStartInCollidersMark = Physics2D.queriesStartInColliders;
        Physics2D.queriesStartInColliders = false;
    }

    public override void Update()
    {
        base.Update();

        Bounds bounds = coll.bounds;
        Vector3 extents = bounds.extents;
        Vector3 right = rootTrans.right;

        RaycastHit2D[] hit2Ds = Tool.RaycastAll2D(bounds.center, right, extents.x, extents.y, 5, LayerHelper.GetLayerMask(layerSize, true));
        if (!Tool.IsCheck(hit2Ds))
        {
            Vector3 newPosition = rootTrans.position;
            newPosition += rootTrans.right * sprintingSpeed * Time.deltaTime;
            if (!Tool.IsOutOfCameraX(newPosition.x, -coll.bounds.extents.x))
            {
                rootTrans.position = newPosition;
            }
        }
    }

    public override void ExitStatus()
    {
        base.ExitStatus();
        Physics2D.queriesStartInColliders = queriesStartInCollidersMark;
    }

    public override bool MeetEnterCondition()
    {
        return base.MeetEnterCondition()
            && (input.IsDoubleKeyDown(KeyCodeIndex.RightKeyCodeIdx) && rootTrans.right.x > 0
            || input.IsDoubleKeyDown(KeyCodeIndex.LeftKeyCodeIdx) && rootTrans.right.x < 0)
            ;
    }

    public override bool MeetExitCondition()
    {
        return base.MeetExitCondition()
            || rootTrans.right.x > 0 && !input.IsKey(KeyCodeIndex.RightKeyCodeIdx)
            || rootTrans.right.x < 0 && !input.IsKey(KeyCodeIndex.LeftKeyCodeIdx)
            ;
    }
}
