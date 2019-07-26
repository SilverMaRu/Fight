using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BeHurtHandler(GameObject rootGO, Part beHurtPart, float damage);
public delegate void DefendSkillHandler(GameObject rootGO, Part defendPart, Skill defendSkill);
public class Part : MonoBehaviour
{
    public static event BeHurtHandler BeHurtEvent;
    public static event DefendSkillHandler DefendSkillEvent;

    private List<GameObject> hitRootGOList = new List<GameObject>();

    // 攻击力
    public float attack;
    // 防御力
    public float defense;
    public GameObject rootGO;
    public string PartName { get; private set; }
    //[HideInInspector]
    public bool isDefend = false;
    private int layerHurtOnly = -1;
    private int layerAttackOnly = -1;

    // Start is called before the first frame update
    void Start()
    {
        PartName = gameObject.name;
        layerHurtOnly = LayerMask.NameToLayer("HurtOnly");
        layerAttackOnly = LayerMask.NameToLayer("AttackOnly");
    }

    public void Hurt(Part attackPart)
    {
        if (isDefend)
        {
            DefendSkillEvent?.Invoke(rootGO, this, null);
        }
        else
        {
            Debug.Log("attackPart.Name = " + attackPart.name);
            Debug.Log("beHurtPart.Name = " + name);
            float damage = attackPart.attack - defense;

            if (LayerHasHurt() && damage >= 0)
            {
                BeHurtEvent(rootGO, this, damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerHasAttack())
        {
            Part hitPart = collision.GetComponent<Part>();
            if (hitPart != null)
            {
                GameObject hitRootGO = hitPart.rootGO;
                if (!hitRootGO.Equals(rootGO) && !hitRootGOList.Contains(hitRootGO))
                {
                    hitPart.Hurt(this);
                    hitRootGOList.Add(hitPart.rootGO);
                }
            }
        }
    }

    private bool LayerHasHurt()
    {
        int myLayer = gameObject.layer;
        return myLayer >= layerHurtOnly && myLayer < layerAttackOnly;
    }

    private bool LayerHasAttack()
    {
        int myLayer = gameObject.layer;
        return myLayer > layerHurtOnly && myLayer <= layerAttackOnly;
    }

    public void UpgradeLayer(int level = 1)
    {
        level = Mathf.Abs(level);
        int newLayer = gameObject.layer + level;
        gameObject.layer = Mathf.Clamp(newLayer, layerHurtOnly, layerAttackOnly);
    }

    public void DowngradeLayer(int level = 1)
    {
        level = Mathf.Abs(level);
        int newLayer = gameObject.layer - level;
        gameObject.layer = Mathf.Clamp(newLayer, layerHurtOnly, layerAttackOnly);
    }

    public void ResetHitGameObjectList()
    {
        hitRootGOList.Clear();
    }
}
