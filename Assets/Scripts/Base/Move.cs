using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private const string FRONT_ANIMATOR_BOOL_NAME = "front";
    private const string SPRINT_ANIMATOR_BOOL_NAME = "sprint";
    private const string BREAK_ANIMATOR_BOOL_NAME = "break";

    public int speed = 5;
    private int Speed
    {
        get
        {
            int result = sprinting? speed * 2:speed;
            return result;
        }
    }
    public FightInput fightInput;
    public Animator anim;
    public Transform leftBorderTrans;
    public Transform rightBorderTrans;
    // 冲刺状态
    private bool sprinting = false;

    private Transform myTrans;

    // Start is called before the first frame update
    void Start()
    {
        if(fightInput == null)
        {
            fightInput = GetComponent<FightInput>();
        }
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        myTrans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = myTrans.position;
        if (myTrans.right.x > 0)
        {
            if (fightInput.rightDouableKeyDownCache.CacheKeyCodes.Length == 2) sprinting = true;
            if (Input.GetKey(fightInput.rightKey))
            {
                newPosition += myTrans.right * Speed * Time.deltaTime;
            }
            
            if (Input.GetKey(fightInput.leftKey))
            {
                sprinting = false;
                newPosition -= myTrans.right * Speed * Time.deltaTime;
            }
        }
        else if (myTrans.right.x < 0)
        {
            if (fightInput.leftDoubleKeyDownCache.CacheKeyCodes.Length == 2) sprinting = true;
            if (Input.GetKey(fightInput.leftKey))
            {
                newPosition += myTrans.right * Speed * Time.deltaTime;
            }

            if (Input.GetKey(fightInput.rightKey))
            {
                sprinting = false;
                newPosition -= myTrans.right * Speed * Time.deltaTime;
            }
        }

        //if(newPosition.x> leftBorderTrans.position.x && newPosition.x < rightBorderTrans.position.x)
        if(!Tool.IsOutOfCameraX(newPosition.x))
        {
            myTrans.position = newPosition;
        }

        if (Input.GetKeyUp(fightInput.rightKey) || Input.GetKeyUp(fightInput.leftKey))
        {
            sprinting = false;
        }
    }
}
