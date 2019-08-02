using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AttributesChangeHandler(float current, float max);
public class FighterAttributesManager : MonoBehaviour
{
    public event AttributesChangeHandler HPChangeEvent;
    public event AttributesChangeHandler MPChangeEvent;
    public event AttributesChangeHandler SPChangeEvent;
    public event AttributesChangeHandler AttackChangeEvent;
    public event AttributesChangeHandler DefenseChangeEvent;
    public event AttributesChangeHandler SpeedChangeEvent;

    private GameObject rootGO;
    private BuffManager buffManager;
    [HideInInspector]
    public FighterAttributes fighterAttr;
    private TimeScale localTimeScale;

    // 生命值
    private float _currentHP;
    public float currentHP
    {
        get { return _currentHP; }
        private set
        {
            _currentHP = Mathf.Clamp(value, 0, fighterAttr.baseHP);
            HPChangeEvent?.Invoke(_currentHP, fighterAttr.baseHP);
        }
    }
    // 生命值恢复量 秒/点
    public float recoverHP { get { return fighterAttr.baseRecoverHP; } }
    // 魔力值
    private float _currentMP;
    public float currentMP
    {
        get { return _currentMP; }
        private set
        {
            _currentMP = Mathf.Clamp(value, 0, fighterAttr.baseMP);
            MPChangeEvent?.Invoke(_currentMP, fighterAttr.baseMP);
        }
    }
    // 魔力值恢复量 (秒/点)
    public float recoverMP { get { return fighterAttr.baseRecoverMP; } }
    // 耐力
    private float _currentSP;
    public float currentSP
    {
        get { return _currentSP; }
        private set
        {
            _currentSP = Mathf.Clamp(value, 0, fighterAttr.baseSP);
            SPChangeEvent?.Invoke(_currentSP, fighterAttr.baseSP);
        }
    }
    // 耐力值恢复量 (秒/点)
    public float recoverSP { get { return fighterAttr.baseRecoverSP; } }
    // 攻击力
    public float attack { get { return fighterAttr.baseAttack; } }
    // 防御力
    public float defense { get { return fighterAttr.baseDefense; } }
    // 速度
    public float speed { get { return fighterAttr.baseSpeed; } }

    // Start is called before the first frame update
    void Start()
    {
        rootGO = gameObject;
        buffManager = GetComponent<BuffManager>();
        ReflashCurrentAttr();

        Part.BeHurtEvent += OnBeHurt;
        Skill.EnterSkillEvent += OnEnterSkill;
        localTimeScale = GetComponent<TimeScale>();
    }

    private void LateUpdate()
    {
        //float deltaTime = Time.deltaTime;
        float deltaTime = Time.deltaTime * localTimeScale.localTimeScaleRatio;
        if (recoverHP != 0 && currentHP < fighterAttr.baseHP && currentHP > 0)
        {
            currentHP += recoverHP * deltaTime;
        }
        if (recoverMP != 0 && currentMP < fighterAttr.baseMP && currentMP > 0)
        {
            currentMP += recoverMP * deltaTime;
        }
        if (recoverSP != 0 && currentSP < fighterAttr.baseSP && currentSP > 0)
        {
            currentSP += recoverSP * deltaTime;
        }
    }

    public void ReflashCurrentAttr()
    {
        _currentHP = fighterAttr.baseHP;
        _currentMP = fighterAttr.baseMP;
        _currentSP = fighterAttr.baseSP;
    }

    private void OnEnterSkill(GameObject useGO, Skill useSkill)
    {
        if (rootGO.Equals(useGO))
        {
            if (useSkill.HPConsumption != 0)
            {
                currentHP -= useSkill.HPConsumption;
            }
            if (useSkill.MPConsumption != 0)
            {
                currentMP -= useSkill.MPConsumption;
            }
            if (useSkill.SPConsumption != 0)
            {
                currentSP -= useSkill.SPConsumption;
            }
        }
    }

    private void OnBeHurt(GameObject rootGO, Part beHurtPart, float damage)
    {
        if (this.rootGO.Equals(rootGO))
        {
            if (damage != 0)
            {
                currentHP -= damage;
            }
        }
    }
}
