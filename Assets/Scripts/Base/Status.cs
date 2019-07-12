using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Status
{
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
    public string animBoolName = string.Empty;

    public GameObject rootGO;
    protected StatusManager myStatusManager;
    protected Animator anim;

    public Status(GameObject rootGO, StatusManager statusManager, Animator anim)
    {
        this.rootGO = rootGO;
        myStatusManager = statusManager;
        this.anim = anim;
    }

    public virtual bool MeetEnterCondition()
    {
        return false;
    }

    public virtual bool MeetExitCondition()
    {
        return true;
    }

    public virtual void EnterStatus()
    {
        if (myStatusManager != null && Equals(myStatusManager.currentStatus)) return;
        if (myStatusManager != null && myStatusManager.currentStatus != null) myStatusManager.currentStatus.ExitStatus();
        if (myStatusManager != null) myStatusManager.currentStatus = this;
        if (anim != null && !string.IsNullOrEmpty(statusName)) anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
    }

    public virtual void ExitStatus()
    {
        if (anim != null && !string.IsNullOrEmpty(statusName)) anim.SetBool(animBoolName, false);
        if (myStatusManager != null) myStatusManager.currentStatus = null;
    }
}
