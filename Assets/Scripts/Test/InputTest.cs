using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    public FightInput fightInput;
    private Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (fightInput == null)
        {
            fightInput = GetComponent<FightInput>();
        }
        if (thisTransform == null)
        {
            thisTransform = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(fightInput.upKey) && fightInput.upDoubleKeyDownCache.CacheKeyCodes.Length == 2)
        //{
        //    thisTransform.position += thisTransform.up * 2f * Time.deltaTime;
        //    Debug.Log("Do UpDoubleDown " + Time.time);
        //}
        //else if (Input.GetKey(fightInput.upKey) && fightInput.upDoubleKeyDownCache.CacheKeyCodes.Length <2)
        //{
        //    thisTransform.position += thisTransform.up * 0.5f * Time.deltaTime;
        //    //Debug.Log("Do UpDown " + Time.time);
        //}
        //if (Input.GetKey(fightInput.downKey) && fightInput.downDoubleKeyDownCache.CacheKeyCodes.Length == 2)
        //{
        //    thisTransform.position -= thisTransform.up * 2f * Time.deltaTime;
        //    Debug.Log("Do DownDoubleDown " + Time.time);
        //}
        //else if (Input.GetKey(fightInput.downKey) && fightInput.downDoubleKeyDownCache.CacheKeyCodes.Length <2)
        //{
        //    thisTransform.position -= thisTransform.up * 0.5f * Time.deltaTime;
        //    //Debug.Log("Do UpDown " + Time.time);
        //}

        //KeyCode[] baseSkillKeyCodes = fightInput.skillKeysCache.CacheDistinctKeyCodes;
        //string keyCodeStr = string.Empty;
        //foreach (KeyCode tempKeyCode in baseSkillKeyCodes)
        //{
        //    keyCodeStr += tempKeyCode.ToString() + "+";
        //}
        //if (!string.IsNullOrEmpty(keyCodeStr))
        //{
        //    keyCodeStr = keyCodeStr.Substring(0, keyCodeStr.Length - 1);
        //    Debug.Log("Do BaseSkill " + keyCodeStr + " " + Time.time);
        //}


        //if (fightInput.IsKey(fightInput.upKey) && fightInput.IsDoubleKeyDown(fightInput.upKey))
        //{
        //    thisTransform.position += thisTransform.up * 2f * Time.deltaTime;
        //    Debug.Log("Do UpDoubleDown " + Time.time);
        //}
        //else if (fightInput.IsKey(fightInput.upKey))
        //{
        //    thisTransform.position += thisTransform.up * 0.5f * Time.deltaTime;
        //    //Debug.Log("Do UpDown " + Time.time);
        //}
        //if (fightInput.IsKey(fightInput.downKey) && fightInput.IsDoubleKeyDown(fightInput.downKey))
        //{
        //    thisTransform.position -= thisTransform.up * 2f * Time.deltaTime;
        //    Debug.Log("Do DownDoubleDown " + Time.time);
        //}
        //else if (fightInput.IsKey(fightInput.downKey))
        //{
        //    thisTransform.position -= thisTransform.up * 0.5f * Time.deltaTime;
        //    //Debug.Log("Do UpDown " + Time.time);
        //}

        //KeyCode[] baseSkillKeyCodes = { fightInput.baseSkillKey1, fightInput.baseSkillKey2 };
        //if (fightInput.IsKeyDown(baseSkillKeyCodes))
        //{
        //    Debug.Log("Do baseSkillKey1 + 2 (fightInput)");
        //}
        ////else 
        //if (fightInput.IsKeyDown(fightInput.baseSkillKey1))
        //{
        //    Debug.Log("Do baseSkillKey1 (fightInput)");
        //}
        ////else 
        //if (fightInput.IsKeyDown(fightInput.baseSkillKey2))
        //{
        //    Debug.Log("Do baseSkillKey2 (fightInput)");
        //}

        if (Input.GetKeyDown(fightInput.baseSkillKey1) && Input.GetKeyDown(fightInput.baseSkillKey2))
        {
            Debug.Log("Do baseSkillKey1 + 2 (Input)");
        }
        if (Input.GetKeyDown(fightInput.baseSkillKey1))
        {
            Debug.Log("Do baseSkillKey1 (Input)");
        }
        if (Input.GetKeyDown(fightInput.baseSkillKey2))
        {
            Debug.Log("Do baseSkillKey2 (Input)");
        }
    }
}
