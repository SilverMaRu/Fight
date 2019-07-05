using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCache
{
    public KeyCode[] CacheKeyCodes
    {
        get
        {
            TimeupReset();
            List<KeyCode> resultList = new List<KeyCode>();
            foreach (KeyCodeTime tempKeyCodeTime in inputCache)
            {
                if (tempKeyCodeTime.time > 0)
                {
                    resultList.Add(tempKeyCodeTime.keyCode);
                }
            }
            return resultList.ToArray();
        }
    }
    public KeyCode[] CacheDistinctKeyCodes
    {
        get
        {
            List<KeyCode> resultList = new List<KeyCode>();
            KeyCode[] cacheKeyCodes = CacheKeyCodes;
            foreach (KeyCode tempKeyCode in cacheKeyCodes)
            {
                if (!resultList.Contains(tempKeyCode))
                {
                    resultList.Add(tempKeyCode);
                }
            }
            return resultList.ToArray();
        }
    }
    class KeyCodeTime
    {
        public float time;
        public KeyCode keyCode = KeyCode.Escape;
    }
    private KeyCode[] numberKeyCodes = new KeyCode[1];
    /// <summary>
    /// 缓冲大小
    /// </summary>
    private int cacheSize = 2;
    /// <summary>
    /// 缓存时长
    /// </summary>
    private float cacheTime = Time.deltaTime * 2;
    private KeyCodeTime[] inputCache = new KeyCodeTime[1];
    private int currentIdx = 0;
    private int CurrentIdx { get { return (currentIdx + cacheSize) % cacheSize; } }
    private bool canHold = false;

    public KeyCache(KeyCode[] numberKeyCodes, float cacheTime, bool canHold = false, int cacheSize = 2)
    {
        this.numberKeyCodes = numberKeyCodes;
        this.canHold = canHold;
        this.cacheSize = cacheSize;
        this.cacheTime = cacheTime;
        inputCache = new KeyCodeTime[cacheSize];
        ResetInputCache();
    }

    private void ResetInputCache()
    {
        for (int i = 0; i < inputCache.Length; i++)
        {
            inputCache[i] = new KeyCodeTime();
            inputCache[i].time = -1;
            inputCache[i].keyCode = KeyCode.Escape;
        }
        currentIdx = 0;
    }

    private void TimeupReset()
    {
        List<KeyCodeTime> inputCacheList = new List<KeyCodeTime>(Tool.Sort(inputCache, (a, b) => { return a.time.CompareTo(b.time); }));
        float bastNewTime = inputCacheList[inputCacheList.Count - 1].time;
        if (bastNewTime > 0 && Time.time - bastNewTime > cacheTime)
        {
            ResetInputCache();
        }
    }

    public void Reflash()
    {
        TimeupReset();
        foreach (KeyCode tempKeyCode in numberKeyCodes)
        {
            if (Input.GetKeyDown(tempKeyCode) || (canHold && Input.GetKey(tempKeyCode)))
            {
                inputCache[CurrentIdx].keyCode = tempKeyCode;
                inputCache[CurrentIdx].time = Time.time;
                currentIdx++;
            }
        }
    }

}

public class FightInput : MonoBehaviour
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode baseSkillKey1 = KeyCode.J;
    public KeyCode baseSkillKey2 = KeyCode.K;
    public KeyCode baseSkillKey3 = KeyCode.L;
    public KeyCode baseSkillKey4 = KeyCode.U;
    public KeyCode baseSkillKey5 = KeyCode.I;
    public KeyCode baseSkillKey6 = KeyCode.O;

    public KeyCache upDoubleKeyDownCache;
    public KeyCache downDoubleKeyDownCache;
    public KeyCache leftDoubleKeyDownCache;
    public KeyCache rightDouableKeyDownCache;
    public KeyCache baseSkillKey1DouableKeyDownCache;
    public KeyCache baseSkillKey2DouableKeyDownCache;
    public KeyCache baseSkillKey3DouableKeyDownCache;
    public KeyCache baseSkillKey4DouableKeyDownCache;
    public KeyCache baseSkillKey5DouableKeyDownCache;
    public KeyCache baseSkillKey6DouableKeyDownCache;
    public KeyCache keysCache;

    private Dictionary<KeyCode, KeyCache> codeCachePairs = new Dictionary<KeyCode, KeyCache>();

    // Start is called before the first frame update
    void Start()
    {
        KeyCode[] upKeyArray = new KeyCode[] { upKey };
        KeyCode[] downKeyArray = new KeyCode[] { downKey };
        KeyCode[] leftKeyArray = new KeyCode[] { leftKey };
        KeyCode[] rightKeyArray = new KeyCode[] { rightKey };
        KeyCode[] baseSkillKey1KeyArray = new KeyCode[] { baseSkillKey1 };
        KeyCode[] baseSkillKey2KeyArray = new KeyCode[] { baseSkillKey2 };
        KeyCode[] baseSkillKey3KeyArray = new KeyCode[] { baseSkillKey3 };
        KeyCode[] baseSkillKey4KeyArray = new KeyCode[] { baseSkillKey4 };
        KeyCode[] baseSkillKey5KeyArray = new KeyCode[] { baseSkillKey5 };
        KeyCode[] baseSkillKey6KeyArray = new KeyCode[] { baseSkillKey6 };
        KeyCode[] baseSkillKeyArray = new KeyCode[] { upKey, downKey, leftKey, rightKey, baseSkillKey1, baseSkillKey2, baseSkillKey3, baseSkillKey4, baseSkillKey5, baseSkillKey6 };

        upDoubleKeyDownCache = new KeyCache(upKeyArray, Time.deltaTime * 10, false);
        downDoubleKeyDownCache = new KeyCache(downKeyArray, Time.deltaTime * 10, false);
        leftDoubleKeyDownCache = new KeyCache(leftKeyArray, Time.deltaTime * 10, false);
        rightDouableKeyDownCache = new KeyCache(rightKeyArray, Time.deltaTime * 10, false);
        baseSkillKey1DouableKeyDownCache = new KeyCache(baseSkillKey1KeyArray, Time.deltaTime * 10, false);
        baseSkillKey2DouableKeyDownCache = new KeyCache(baseSkillKey2KeyArray, Time.deltaTime * 10, false);
        baseSkillKey3DouableKeyDownCache = new KeyCache(baseSkillKey3KeyArray, Time.deltaTime * 10, false);
        baseSkillKey4DouableKeyDownCache = new KeyCache(baseSkillKey4KeyArray, Time.deltaTime * 10, false);
        baseSkillKey5DouableKeyDownCache = new KeyCache(baseSkillKey5KeyArray, Time.deltaTime * 10, false);
        baseSkillKey6DouableKeyDownCache = new KeyCache(baseSkillKey6KeyArray, Time.deltaTime * 10, false);
        keysCache = new KeyCache(baseSkillKeyArray, Time.deltaTime * 5, false, baseSkillKeyArray.Length);

        codeCachePairs.Add(upKey, upDoubleKeyDownCache);
        codeCachePairs.Add(downKey, downDoubleKeyDownCache);
        codeCachePairs.Add(leftKey, leftDoubleKeyDownCache);
        codeCachePairs.Add(rightKey, rightDouableKeyDownCache);
        codeCachePairs.Add(baseSkillKey1, upDoubleKeyDownCache);
        codeCachePairs.Add(baseSkillKey2, upDoubleKeyDownCache);
        codeCachePairs.Add(baseSkillKey3, upDoubleKeyDownCache);
        codeCachePairs.Add(baseSkillKey4, upDoubleKeyDownCache);
        codeCachePairs.Add(baseSkillKey5, upDoubleKeyDownCache);
        codeCachePairs.Add(baseSkillKey6, upDoubleKeyDownCache);
    }

    // Update is called once per frame
    void Update()
    {
        upDoubleKeyDownCache.Reflash();
        downDoubleKeyDownCache.Reflash();
        leftDoubleKeyDownCache.Reflash();
        rightDouableKeyDownCache.Reflash();
        baseSkillKey1DouableKeyDownCache.Reflash();
        baseSkillKey2DouableKeyDownCache.Reflash();
        baseSkillKey3DouableKeyDownCache.Reflash();
        baseSkillKey4DouableKeyDownCache.Reflash();
        baseSkillKey5DouableKeyDownCache.Reflash();
        baseSkillKey6DouableKeyDownCache.Reflash();
        keysCache.Reflash();
    }

    //public bool IsKeyDown(KeyCode keyCode)
    //{

    //}

    public bool IsKeyDown(params KeyCode[] keyCodes)
    {
        return EqualsKeyCodes(Tool.Sort(keyCodes), Tool.Sort(keysCache.CacheDistinctKeyCodes));
    }

    private bool EqualsKeyCodes(KeyCode[] sortKeyCodes0, KeyCode[] sortKeyCodes1)
    {
        bool equals = true;
        if (sortKeyCodes0.Length != sortKeyCodes1.Length)
        {
            equals = false;
        }
        else
        {
            for (int i = 0; i < sortKeyCodes1.Length; i++)
            {
                equals = equals && sortKeyCodes0[i] == sortKeyCodes1[i];
                if (!equals)
                {
                    break;
                }
            }
        }
        return equals;
    }

    public bool IsKey(KeyCode keyCode)
    {
        return Input.GetKey(keyCode);
    }

    public bool IsKeyUp(KeyCode keyCode)
    {
        return Input.GetKeyUp(keyCode);
    }

    public bool IsDoubleKeyDown(KeyCode keyCode)
    {
        bool result = false;

        if (codeCachePairs.ContainsKey(keyCode))
        {
            result = codeCachePairs[keyCode].CacheKeyCodes.Length == 2;
        }
        return result;
    }

}
