using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_TimeScaleTest : StatusSkill
{
    public float myScale = 1.25f;
    public float otherScale = 0.8f;
    protected override float stayTime
    {
        get
        {
            //Debug.Log("myTimeScale.localTimeScale = " + myTimeScale.localTimeScale);
            //Debug.Log("TimeScale.worldTimeScaleRatio = " + TimeScale.worldTimeScaleRatio);
            //Debug.Log("stayTime = " + (Time.time - enterTime) * TimeScale.worldTimeScaleRatio);
            return (Time.time - enterTime) * TimeScale.worldTimeScaleRatio;
        }
    }
    private TimeScale myTimeScale;
    private TimeScale[] allTimeScale
    {
        get
        {
            return TimeScale.GetAllTimeScale();
        }
    }
    private TimeScale[] otherTimeScale
    {
        get
        {
            TimeScale[] allTimeScale = this.allTimeScale;
            int length = allTimeScale.Length - 1;
            List<TimeScale> resultList = new List<TimeScale>(length);
            foreach (TimeScale tempTS in allTimeScale)
            {
                if (!tempTS.gameObject.Equals(rootGO))
                {
                    resultList.Add(tempTS);
                }
            }
            return resultList.ToArray();
        }
    }

    public Skill_TimeScaleTest(GameObject rootGO, SkillManager skillManager, Animator anim) : base(rootGO, skillManager, anim)
    {
        myTimeScale = rootGO.GetComponent<TimeScale>();
    }

    public override bool MeetEnterCondition()
    {
        return base.MeetEnterCondition()
            && input.IsKeyDown(orderKeyCodeIdxs)
            ;
    }

    public override void EnterSkill()
    {
        Debug.Log("Do Skill_TimeScaleTest EnterSkill");
        base.EnterSkill();
        ExitSkill();
    }

    protected override void EnterStatusSkill()
    {
        base.EnterStatusSkill();
        myTimeScale.localTimeScale *= myScale;
        TimeScale[] otherTimeScale = this.otherTimeScale;
        foreach (TimeScale tempTS in otherTimeScale)
        {
            tempTS.localTimeScale *= otherScale;
        }
    }

    protected override void ExitStatusSkill()
    {
        base.ExitStatusSkill();
        myTimeScale.localTimeScale /= myScale;
        TimeScale[] otherTimeScale = this.otherTimeScale;
        foreach (TimeScale tempTS in otherTimeScale)
        {
            tempTS.localTimeScale /= otherScale;
        }
    }
}
