using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ForwardAvoid : Skill
{
    public float moveSpeed;

    private Transform rootTrans;
    private Collider2D rootColl;
    private Collider2D[] partColls;
    private LookAt lookAt;
    private int layerSize = -1;

    private float enterTime = -1;

    public Skill_ForwardAvoid(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
        moveSpeed = 2f;

        rootTrans = rootGO.transform;
        rootColl = rootGO.GetComponent<Collider2D>();
        Part[] parts = rootTrans.GetComponentsInChildren<Part>();
        int length = parts.Length;
        partColls = new Collider2D[length];
        for (int i = 0; i < length; i++)
        {
            partColls[i] = parts[i].GetComponent<Collider2D>();
        }
        lookAt = rootGO.GetComponent<LookAt>();

        layerSize = LayerMask.NameToLayer("Size");
    }

    public override bool MeetEnterCondition()
    {
        return base.MeetEnterCondition()
            && (rootTrans.right.x > 0 && input.IsKey(KeyCodeIndex.RightKeyCodeIdx) || rootTrans.right.x < 0 && input.IsKey(KeyCodeIndex.LeftKeyCodeIdx))
            && input.IsKeyDown(orderKeyCodeIdxs);
    }

    public override void EnterSkill()
    {
        base.EnterSkill();
        SwitchAllCollider2D(false);
        enterTime = Time.time;
    }

    public override void UpdateSkill()
    {
        base.UpdateSkill();

        Vector3 newPosition = rootTrans.position;
        newPosition += rootTrans.right * moveSpeed * Time.deltaTime;
        if (!Tool.IsOutOfCameraX(newPosition.x, -rootColl.bounds.extents.x))
        {
            rootTrans.position = newPosition;
        }
        if (Time.time - enterTime > 0.75f)
        {
            ExitSkill();
        }
    }

    public override void ExitSkill()
    {
        base.ExitSkill();
        RaycastHit2D[] hits = null;
        if (IsOverlapping(out hits))
        {
            CrowdOut(rootTrans, rootColl, hits[0].transform, hits[0].collider);
        }

        SwitchAllCollider2D(true);
        enterTime = -1;
    }

    private void SwitchAllCollider2D(bool enable)
    {
        foreach (Collider2D tempPartColl in partColls)
        {
            tempPartColl.enabled = enable;
        }
    }

    private bool IsOverlapping(out RaycastHit2D[] hits)
    {
        bool isOverlapping = false;
        int rootLayerMark = rootGO.layer;
        rootGO.layer = Physics2D.IgnoreRaycastLayer;
        int layerMask = LayerHelper.GetLayerMask(layerSize, true);

        List<RaycastHit2D> hitList = new List<RaycastHit2D>();
        Bounds bounds = rootColl.bounds;
        Vector3 center = bounds.center;
        float halfWidth = bounds.extents.x;
        float halfHeight = bounds.extents.y;
        Vector3 right = rootTrans.right;
        int halfRayNum = 5;

        hits = Tool.RaycastAll2D(center - right * halfWidth, right, halfWidth * 2, halfHeight, halfRayNum, layerMask);
        hitList.AddRange(hits);
        hits = Tool.RaycastAll2D(center + right * halfWidth, -right, halfWidth * 2, halfHeight, halfRayNum, layerMask);
        hitList.AddRange(hits);
        hits = hitList.ToArray();

        isOverlapping = Tool.IsCheck(hits);
        
        rootGO.layer = rootLayerMark;
        return isOverlapping;
    }

    // 排挤(调整重叠的两个对象) one指代自己
    private void CrowdOut(Transform oneTrans, Collider2D oneColl, Transform otherTrans, Collider2D otherColl)
    {
        //Vector3 myToHit = otherTrans.position - oneTrans.position;
        Vector3 myToHit = otherColl.bounds.center - oneColl.bounds.center;
        float myToHitX = Vector3.Scale(myToHit, Tool.flatXZ).x;
        float distance = Mathf.Abs(myToHitX);
        // 指示该游戏对象位于另一游戏对象的左侧还是右侧  -1为左  1为右
        float leftOrRight = distance == 0 ? oneTrans.right.x : -myToHitX / distance;
        // 两个对象正常距离
        float normDistance = otherColl.bounds.extents.x + oneColl.bounds.extents.x;
        Vector3 rootNewPosition = otherTrans.position + Vector3.right * leftOrRight * normDistance;
        if (Tool.IsOutOfCameraX(rootNewPosition.x, -oneColl.bounds.extents.x))
        {
            oneTrans.position = otherTrans.position - Vector3.right * leftOrRight * normDistance;
        }
        else
        {
            oneTrans.position = rootNewPosition;
        }
    }
}
