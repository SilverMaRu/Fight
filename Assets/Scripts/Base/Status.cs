using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Status
{
    public delegate void EnterStatusHandler(GameObject rootGO, Status enterStatus);
    public delegate void ExitStatusHandler(GameObject rootGO, Status exitStatus);
    public static event EnterStatusHandler EnterStatusEvent;
    public static event ExitStatusHandler ExitStatusEvent;

    private static Type[] statusTypes;
    public static Type[] StatusTypes
    {
        get
        {
            Type[] resultArray = null;
            if (statusTypes == null)
            {
                statusTypes = TypeHelper.GetSubclassesOf(typeof(Status));
            }
            if (statusTypes != null)
            {
                resultArray = new Type[statusTypes.Length];
                statusTypes.CopyTo(resultArray, 0);
            }
            return resultArray;
        }
    }

    public string statusName = string.Empty;
    public bool onEnable = true;
    public GameObject rootGO;
    protected StatusManager myStatusManager;
    protected Animator anim;
    protected FightInput input;

    public Status(GameObject rootGO, StatusManager statusManager, Animator anim)
    {
        this.rootGO = rootGO;
        myStatusManager = statusManager;
        this.anim = anim;
        input = myStatusManager.fightInput;
    }

    public virtual bool MeetEnterCondition()
    {
        return onEnable
            && SkillManager.GetCurrentSkill(rootGO) == null;
            ;
    }

    public virtual bool MeetExitCondition()
    {
        return !onEnable
            || SkillManager.GetCurrentSkill(rootGO) != null
            ;
    }

    public virtual void EnterStatus()
    {
        if (myStatusManager == null) return;
        if (Equals(myStatusManager.currentStatus)) return;
        if (myStatusManager.currentStatus != null) myStatusManager.currentStatus.ExitStatus();
        myStatusManager.currentStatus = this;
        if (anim != null && !string.IsNullOrEmpty(statusName)) anim.SetBool(statusName, true);
        EnterStatusEvent?.Invoke(rootGO, this);
    }

    public virtual void Update()
    {
    }

    public virtual void ExitStatus()
    {
        if (anim != null && !string.IsNullOrEmpty(statusName)) anim.SetBool(statusName, false);
        if (myStatusManager != null) myStatusManager.currentStatus = null;
        ExitStatusEvent?.Invoke(rootGO, this);
    }
}
