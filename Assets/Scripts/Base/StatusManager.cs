using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    private string[] testStatusTypes = new string[] { "Status_Sprinting", "Status_WalkForward", "Status_WalkBack" };

    public FightInput fightInput;
    public Animator anim;

    [HideInInspector]
    public Status currentStatus;
    private Status[] myStatus;

    // Start is called before the first frame update
    void Start()
    {
        myStatus = CreateMyStatus(testStatusTypes);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStatus == null)
        {
            foreach (Status tempStatus in myStatus)
            {
                if (tempStatus.MeetEnterCondition())
                {
                    tempStatus.EnterStatus();
                    break;
                }
            }
        }

        if (currentStatus != null)
        {
            currentStatus.Update();
            if (currentStatus.MeetExitCondition()) currentStatus.ExitStatus();
        }
    }

    private Status[] CreateMyStatus(string[] statusTypeNames)
    {
        int length = statusTypeNames.Length;
        Status[] resultArray = new Status[length];
        List<System.Type> skillTypes = new List<System.Type>(Status.StatusTypes);
        System.Type[] constructorTypes = new System.Type[] { typeof(GameObject), typeof(StatusManager), typeof(Animator) };
        object[] constructorInvokeObjs = new object[] { gameObject, this, anim };
        for (int i = 0; i < length; i++)
        {
            System.Type tempSkillType = TypeHelper.GetType(skillTypes.ToArray(), statusTypeNames[i]);
            resultArray[i] = (Status)tempSkillType.GetConstructor(constructorTypes)?.Invoke(constructorInvokeObjs);
            skillTypes.Remove(tempSkillType);
        }
        return resultArray;
    }
}
