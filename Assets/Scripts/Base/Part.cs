using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    // 攻击力
    public float attack;
    // 防御力
    public float defense;
    public GameObject rootGO;
    public string PartName { get; private set; }
    private int layerHurtOnly = -1;
    private int layerAttackOnly = -1;

    // Start is called before the first frame update
    void Start()
    {
        PartName = gameObject.name;
        layerHurtOnly = LayerHelper.GetLayer("HurtOnly");
        layerAttackOnly = LayerHelper.GetLayer("AttackOnly");
    }

    public void Hurt(float damage)
    {
        Debug.Log("Be Hurt, Damage: " + damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerHasAttack())
        {
            Part hitPart = collision.GetComponent<Part>();
            if (hitPart != null && !hitPart.rootGO.Equals(rootGO))
            {
                hitPart.Hurt(attack);
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
}
