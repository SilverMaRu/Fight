using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static Dictionary<GameObject, SkillManager> goManagerPairs = new Dictionary<GameObject, SkillManager>();

    private static System.Type typeOfGameObject = typeof(GameObject);
    private static System.Type typeOfSkillManager = typeof(SkillManager);
    private static System.Type typeOfAnimator = typeof(Animator);

    public bool listeningInput = true;
    // 角色可用技能列表
    [HideInInspector]
    public string[] testSkillTypes = new string[] { "Skill_BoxingCombo_1", "Skill_BoxingCombo_2", "Skill_TestEscape", "Skill_Block", "Skill_ForwardAvoid" };
    // 当前技能
    [HideInInspector]
    public Skill currentSkill;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public FightInput fightInput;

    // 身上的技能列表
    private Skill[] mySkills;
    // 起手技能列表
    private Skill[] rootSkills;
    // 监听中的(可能接续的)技能列表
    private Skill[] listeningSkills;
    private int layerHurtOnly = -1;
    private int layerAttackOnly = -1;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fightInput = GetComponent<FightInput>();
        mySkills = CreateMySkills(testSkillTypes);
        rootSkills = SelectRootSkills(mySkills);

        layerHurtOnly = LayerMask.NameToLayer("HurtOnly");
        layerAttackOnly = LayerMask.NameToLayer("AttackOnly");

        goManagerPairs.Add(gameObject, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (listeningInput)
        {
            if (currentSkill == null && listeningSkills != rootSkills)
            {
                listeningSkills = rootSkills;
            }
            else if (currentSkill != null && listeningSkills != currentSkill.deriveSkills)
            {
                listeningSkills = currentSkill.deriveSkills;
            }

            foreach (Skill tempSkill in listeningSkills)
            {
                // 如果输入指令与设定指令相同
                if (tempSkill.MeetEnterCondition())
                {
                    tempSkill.EnterSkill();
                }
            }
        }
        if (currentSkill != null)
        {
            currentSkill.UpdateSkill();
        }
    }

    private Skill[] CreateMySkills(string[] skillTypeNames)
    {
        int length = skillTypeNames.Length;
        Skill[] resultArray = new Skill[length];
        List<System.Type> skillTypes = new List<System.Type>(Skill.SkillTypes);
        System.Type[] constructorTypes = new System.Type[] { typeOfGameObject, typeOfSkillManager, typeOfAnimator };
        object[] constructorInvokeObjs = new object[] { gameObject, this, anim };
        for (int i = 0; i < length; i++)
        {
            System.Type tempSkillType = TypeHelper.GetType(skillTypes.ToArray(), skillTypeNames[i]);
            resultArray[i] = (Skill)tempSkillType.GetConstructor(constructorTypes)?.Invoke(constructorInvokeObjs);
            skillTypes.Remove(tempSkillType);
        }

        foreach (Skill tempSkill in resultArray)
        {
            tempSkill.MatchFrontSkills(resultArray);
            tempSkill.MatchDeriveSkills(resultArray);
        }
        return resultArray;
    }

    private Skill[] SelectRootSkills(Skill[] skills)
    {
        List<Skill> resultSkillList = new List<Skill>();
        foreach (Skill tempSkill in skills)
        {
            Skill[] frontSkills = tempSkill.frontSkills;
            if (frontSkills == null || frontSkills.Length == 0 || tempSkill.IsMyFrontSkill(null))
            {
                resultSkillList.Add(tempSkill);
            }
        }
        return resultSkillList.ToArray();
    }

    public void OpenListeningInput()
    {
        listeningInput = true;
    }

    public void ClossListeningInput()
    {
        listeningInput = false;
    }

    // 打开部位影响
    public void EnableInfluence(string influenceGOName)
    {
        if (currentSkill != null) currentSkill.EnableInfluence(influenceGOName);
    }

    // 关闭部位影响
    public void DisableInfluence(string influenceGOName)
    {
        if (currentSkill != null) currentSkill.DisableInfluence(influenceGOName);
    }

    public void EndCombo()
    {
        if (currentSkill != null) currentSkill.ExitSkill();
    }

    public static Skill GetCurrentSkill(GameObject rootGO)
    {
        Skill resultSkill = null;
        SkillManager thisSkillManager = null;
        if (goManagerPairs.TryGetValue(rootGO, out thisSkillManager))
        {
            resultSkill = thisSkillManager.currentSkill;
        }
        return resultSkill;
    }
}
