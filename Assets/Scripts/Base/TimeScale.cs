using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LocalTimeScaleChangeHandler();
public class TimeScale : MonoBehaviour
{
    // 游戏程序中所有TimeScale
    private static Dictionary<GameObject, TimeScale> goTSPairs = new Dictionary<GameObject, TimeScale>();
    public event LocalTimeScaleChangeHandler LocalTimeScaleChangeEvent;
    private float _localTimeScale = 1;
    // 游戏对象的时间尺度
    public float localTimeScale
    {
        get
        {
            return _localTimeScale;
        }
        set
        {
            if (value < 0) _localTimeScale = 0;
            else _localTimeScale = value;
            anim.speed = localTimeScaleRatio;
            LocalTimeScaleChangeEvent?.Invoke();
        }
    }
    // 游戏对象的时间尺度比上游戏程序的时间尺度 localTimeScale/Time.timeScale
    public float localTimeScaleRatio { get { return localTimeScale / Time.timeScale; } }
    // 从所有TimeScale中选择localTimeScale最小的一个比上游戏程序的时间尺度, 用于倒计时等公共对象
    public static float worldTimeScaleRatio
    {
        get
        {
            float result = 1;
            int length = goTSPairs.Count;
            TimeScale[] allTimeScale = GetAllTimeScale();
            float[] localTimeScales = new float[length];
            for (int i = 0; i < length; i++)
            {
                localTimeScales[i] = allTimeScale[i].localTimeScale;
            }
            result = Mathf.Min(localTimeScales) / Time.timeScale;
            return result;
        }
    }

    private Animator anim;

    private void Awake()
    {
        goTSPairs.Add(gameObject, this);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    #region Test
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.KeypadPlus))
    //    {
    //        localTimeScale += 0.1f;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.KeypadMinus))
    //    {
    //        localTimeScale -= 0.1f;
    //    }
    //}
    #endregion Test

    public static TimeScale GetTimeScale(GameObject rootGO)
    {
        TimeScale result = null;
        goTSPairs.TryGetValue(rootGO, out result);
        return result;
    }

    public static TimeScale[] GetAllTimeScale()
    {
        TimeScale[] resultArray = new TimeScale[goTSPairs.Count];
        goTSPairs.Values.CopyTo(resultArray, 0);
        return resultArray;
    }
}
