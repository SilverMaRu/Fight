using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private string[] testSkillTypes = new string[] { "Skill_BoxingCombo_1", "Skill_BoxingCombo_2" };

    public bool listeningInput = true;
    public Animator anim;
    public FightInput fightInput;
    // 当前技能
    [HideInInspector]
    public Skill currentSkill;
    [HideInInspector]
    public Skill_2 currentSkill_2;
    // 身上的技能列表
    private Skill[] mySkills;
    private Skill_2[] mySkills_2;
    // 起手技能列表
    private Skill[] rootSkills;
    private Skill_2[] rootSkills_2;
    // 监听中的(可能接续的)技能列表
    private Skill[] listeningSkills;
    private Skill_2[] listeningSkill_2s;
    private int layerHurtOnly = -1;
    private int layerAttackOnly = -1;

    // Start is called before the first frame update
    void Start()
    {
        fightInput = GetComponent<FightInput>();

        //mySkills = GetComponents<Skill>();
        //if (mySkills != null)
        //{
        //    rootSkills = SelectRootSkills(mySkills);
        //}
        mySkills_2 = CreateMySkills(testSkillTypes);
        rootSkills_2 = SelectRootSkills(mySkills_2);

        layerHurtOnly = LayerHelper.GetLayer("HurtOnly");
        layerAttackOnly = LayerHelper.GetLayer("AttackOnly");
    }

    // Update is called once per frame
    void Update()
    {
        if (!listeningInput) return;
        //if (currentSkill == null && listeningSkills != rootSkills)
        //{
        //    listeningSkills = rootSkills;
        //}
        //else if (currentSkill != null && listeningSkills != currentSkill.deriveSkills)
        //{
        //    listeningSkills = currentSkill.deriveSkills;
        //}
        //foreach (Skill tempSkill in listeningSkills)
        //{
        //    // 如果输入指令与设定指令相同
        //    if (fightInput.IsKeyDown(tempSkill.orderKeyCodeIdxs))
        //    {
        //        listeningInput = false;
        //        tempSkill.EnterSkill();
        //        currentSkill = tempSkill;
        //    }
        //}


        if (currentSkill_2 == null && listeningSkill_2s != rootSkills_2)
        {
            listeningSkill_2s = rootSkills_2;
        }
        else if (currentSkill_2 != null && listeningSkill_2s != currentSkill_2.deriveSkills)
        {
            listeningSkill_2s = currentSkill_2.deriveSkills;
        }
        Debug.Log("listeningSkill_2s.Length = " + listeningSkill_2s.Length);
        foreach (Skill_2 tempSkill in listeningSkill_2s)
        {
            // 如果输入指令与设定指令相同
            if (tempSkill.MeetEnterCondition())
            {
                listeningInput = false;
                tempSkill.EnterSkill();
            }
        }
    }

    private Skill_2[] CreateMySkills(string[] skillTypeNames)
    {
        int length = skillTypeNames.Length;
        Skill_2[] resultArray = new Skill_2[length];
        List<System.Type> skillTypes = new List<System.Type>(Skill_2.SkillTypes);
        System.Type[] constructorTypes = new System.Type[] { typeof(GameObject), typeof(SkillManager), typeof(Animator) };
        object[] constructorInvokeObjs = new object[] { gameObject, this, anim };
        for (int i = 0; i < length; i++)
        {
            System.Type tempSkillType = TypeHelper.GetType(skillTypes.ToArray(), skillTypeNames[i]);
            resultArray[i] = (Skill_2)tempSkillType.GetConstructor(constructorTypes)?.Invoke(constructorInvokeObjs);
            skillTypes.Remove(tempSkillType);
        }

        foreach (Skill_2 tempSkill in resultArray)
        {
            tempSkill.SetFrontSkills(resultArray);
            tempSkill.SetDeriveSkills(resultArray);
        }
        return resultArray;
    }

    private Skill_2[] SelectRootSkills(Skill_2[] skills)
    {
        List<Skill_2> resultSkillList = new List<Skill_2>();
        foreach (Skill_2 tempSkill in skills)
        {
            Skill_2[] frontSkills = tempSkill.frontSkills;
            if (frontSkills == null || frontSkills.Length == 0 || HasNullItem(frontSkills))
            {
                resultSkillList.Add(tempSkill);
            }
        }
        return resultSkillList.ToArray();
    }

    private Skill[] SelectRootSkills(Skill[] skills)
    {
        List<Skill> resultSkillList = new List<Skill>();
        foreach (Skill tempSkill in skills)
        {
            Skill[] frontSkills = tempSkill.frontSkills;
            if (frontSkills == null || frontSkills.Length == 0 || HasNullItem(frontSkills))
            {
                resultSkillList.Add(tempSkill);
            }
        }
        return resultSkillList.ToArray();
    }

    private bool HasNullItem<T>(T[] array)
    {
        bool hasNullItem = false;
        foreach (T tempItem in array)
        {
            if (tempItem == null)
            {
                hasNullItem = true;
                break;
            }
        }
        return hasNullItem;
    }

    public void OpenListeningInput()
    {
        listeningInput = true;
    }

    public void ClossListeningInput()
    {
        listeningInput = false;
    }

    // 打开攻击判定
    public void OpenAttackDecision(string gameObjectName)
    {
        //GameObject attackGO = currentSkill.GetAttackGameObjcet(gameObjectName);
        GameObject attackGO = currentSkill_2.GetAttackGameObjcet(gameObjectName);
        int layerValue = attackGO.layer;
        layerValue = Mathf.Clamp(++layerValue, layerHurtOnly, layerAttackOnly);
        attackGO.layer = layerValue;
    }

    // 关闭攻击判定
    public void ClossAttackDecision(string gameObjectName)
    {
        //GameObject attackGO = currentSkill.GetAttackGameObjcet(gameObjectName);
        GameObject attackGO = currentSkill_2.GetAttackGameObjcet(gameObjectName);
        int layerValue = attackGO.layer;
        layerValue = Mathf.Clamp(--layerValue, layerHurtOnly, layerAttackOnly);
        attackGO.layer = layerValue;
    }

    public void EndCombo()
    {
        //currentSkill.ExitSkill();
        //currentSkill = null;

        currentSkill_2.ExitSkill();

    }
}
