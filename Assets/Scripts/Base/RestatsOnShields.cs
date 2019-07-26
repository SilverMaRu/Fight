using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestatsOnShields : Status
{
    protected float durationTime = 1;
    protected float enterTime = 0;
    protected bool exit = false;
    protected System.Type typeOfSkillEsc = typeof(Skill_TestEscape);

    //private Transform trans;
    //private Collider2D coll2D;
    //private int layerGround = -1;

    public RestatsOnShields(GameObject rootGO, StatusManager statusManager, Animator anim) : base(rootGO, statusManager, anim)
    {
        //trans = rootGO.transform;
        //coll2D = rootGO.GetComponent<Collider2D>();
        //layerGround = LayerMask.NameToLayer("Ground");

        Part.BeHurtEvent += OnBeHurt;
        Skill.EnterSkillEvent += OnEnterSkill;
    }

    public override bool MeetEnterCondition()
    {
        return false;
    }

    public override void EnterStatus()
    {
        base.EnterStatus();
        enterTime = Time.time;
    }

    //public override void Update()
    //{
    //    base.Update();
    //    if (!IsOnGround())
    //    {

    //    }
    //}

    public override bool MeetExitCondition()
    {
        return Time.time - enterTime > durationTime;
    }

    public virtual void SetDurationTime(float durationTime)
    {
        this.durationTime = Mathf.Abs(durationTime);
    }

    protected virtual void OnBeHurt(GameObject rootGO, Part beHurtPart, float damage)
    {
        if (rootGO.Equals(this.rootGO))
        {
            EnterStatus();
        }
    }

    protected virtual void OnEnterSkill(GameObject rootGO, Skill useSkill)
    {
        if (rootGO.Equals(this.rootGO) && useSkill.GetType().IsSubclassOf(typeOfSkillEsc))
        {
            ExitStatus();
        }
    }

    //private bool IsOnGround()
    //{
    //    bool isOnGround = false;
    //    List<RaycastHit2D> hit2DList = new List<RaycastHit2D>();
    //    Bounds bounds = coll2D.bounds;
    //    Vector3 center = bounds.center;
    //    float extensX = bounds.extents.x;
    //    float extensY = bounds.extents.y;
    //    int layerMask = LayerHelper.GetLayerMask(layerGround, true);
    //    Vector3 up = trans.up;
    //    Vector3 right = trans.right;
    //    hit2DList.AddRange(Tool.RaycastAll2D(center, -up, extensY, extensX, 3, layerMask));
    //    hit2DList.AddRange(Tool.RaycastAll2D(center, up, extensY, extensX, 3, layerMask));
    //    hit2DList.AddRange(Tool.RaycastAll2D(center, right, extensX, extensY, 6, layerMask));
    //    hit2DList.AddRange(Tool.RaycastAll2D(center, -right, extensX, extensY, 6, layerMask));
    //    isOnGround = Tool.IsCheck(hit2DList.ToArray());
    //    return isOnGround;
    //}
}
