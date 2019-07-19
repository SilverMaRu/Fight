using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    private string[] testStatusTypes = new string[] { "Status_Sprinting", "Status_WalkForward", "Status_WalkBack", "Status_Idle", "Status_BeHurt" };
    private static Dictionary<GameObject, StatusManager> goManagerPairs = new Dictionary<GameObject, StatusManager>();

    private static System.Type typeOfGameObject = typeof(GameObject);
    private static System.Type typeOfStatusManager = typeof(StatusManager);
    private static System.Type typeOfAnimator = typeof(Animator);

    [HideInInspector]
    public FightInput fightInput;
    [HideInInspector]
    public Animator anim;

    [HideInInspector]
    public Status currentStatus;
    private Status[] myStatus;

    // Start is called before the first frame update
    void Start()
    {
        fightInput = GetComponent<FightInput>();
        anim = GetComponent<Animator>();
        myStatus = CreateMyStatus(testStatusTypes);

        goManagerPairs.Add(gameObject, this);
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
        System.Type[] constructorTypes = new System.Type[] { typeOfGameObject, typeOfStatusManager, typeOfAnimator };
        object[] constructorInvokeObjs = new object[] { gameObject, this, anim };
        for (int i = 0; i < length; i++)
        {
            System.Type tempSkillType = TypeHelper.GetType(skillTypes.ToArray(), statusTypeNames[i]);
            resultArray[i] = (Status)tempSkillType.GetConstructor(constructorTypes)?.Invoke(constructorInvokeObjs);
            skillTypes.Remove(tempSkillType);
        }
        return resultArray;
    }

    public void EnterRestatsOnShields(System.Type type)
    {
        foreach(Status tempStatus in myStatus)
        {
            if (tempStatus.GetType().Equals(type))
            {
                tempStatus.EnterStatus();
            }
        }
    }

    public static Status GetCurrentStatus(GameObject rootGO)
    {
        Status resultStatus = null;
        StatusManager thisStatusManager = null;
        if(goManagerPairs.TryGetValue(rootGO, out thisStatusManager))
        {
            resultStatus = thisStatusManager.currentStatus;
        }
        return resultStatus;
    }
}
