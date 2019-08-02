using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FightInput : MonoBehaviour
{
    [HideInInspector]
    public PlayerInputInfo inputInfo;
    private KeyCode[] allKeyCodes;

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

    private void Start()
    {
        allKeyCodes = new KeyCode[(int)KeyCodeIndex.KeyCodeLength];
        allKeyCodes[(int)KeyCodeIndex.UpKeyCodeIdx] = inputInfo.upKey;
        allKeyCodes[(int)KeyCodeIndex.DownKeyCodeIdx] = inputInfo.downKey;
        allKeyCodes[(int)KeyCodeIndex.LeftKeyCodeIdx] = inputInfo.leftKey;
        allKeyCodes[(int)KeyCodeIndex.RightKeyCodeIdx] = inputInfo.rightKey;
        allKeyCodes[(int)KeyCodeIndex.BaseSkillKeyCodeIdx1] = inputInfo.baseSkillKey1;
        allKeyCodes[(int)KeyCodeIndex.BaseSkillKeyCodeIdx2] = inputInfo.baseSkillKey2;
        allKeyCodes[(int)KeyCodeIndex.BaseSkillKeyCodeIdx3] = inputInfo.baseSkillKey3;
        allKeyCodes[(int)KeyCodeIndex.BaseSkillKeyCodeIdx4] = inputInfo.baseSkillKey4;
        allKeyCodes[(int)KeyCodeIndex.BaseSkillKeyCodeIdx5] = inputInfo.baseSkillKey5;
        allKeyCodes[(int)KeyCodeIndex.BaseSkillKeyCodeIdx6] = inputInfo.baseSkillKey6;

        KeyCode[] upKeyArray = GetKeyCodes(KeyCodeIndex.UpKeyCodeIdx);
        KeyCode[] downKeyArray = GetKeyCodes(KeyCodeIndex.DownKeyCodeIdx);
        KeyCode[] leftKeyArray = GetKeyCodes(KeyCodeIndex.LeftKeyCodeIdx);
        KeyCode[] rightKeyArray = GetKeyCodes(KeyCodeIndex.RightKeyCodeIdx);
        KeyCode[] baseSkillKey1KeyArray = GetKeyCodes(KeyCodeIndex.BaseSkillKeyCodeIdx1);
        KeyCode[] baseSkillKey2KeyArray = GetKeyCodes(KeyCodeIndex.BaseSkillKeyCodeIdx2);
        KeyCode[] baseSkillKey3KeyArray = GetKeyCodes(KeyCodeIndex.BaseSkillKeyCodeIdx3);
        KeyCode[] baseSkillKey4KeyArray = GetKeyCodes(KeyCodeIndex.BaseSkillKeyCodeIdx4);
        KeyCode[] baseSkillKey5KeyArray = GetKeyCodes(KeyCodeIndex.BaseSkillKeyCodeIdx5);
        KeyCode[] baseSkillKey6KeyArray = GetKeyCodes(KeyCodeIndex.BaseSkillKeyCodeIdx6);

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
        keysCache = new KeyCache(allKeyCodes, Time.deltaTime * 3, false, allKeyCodes.Length);

        codeCachePairs.Add(inputInfo.upKey, upDoubleKeyDownCache);
        codeCachePairs.Add(inputInfo.downKey, downDoubleKeyDownCache);
        codeCachePairs.Add(inputInfo.leftKey, leftDoubleKeyDownCache);
        codeCachePairs.Add(inputInfo.rightKey, rightDouableKeyDownCache);
        codeCachePairs.Add(inputInfo.baseSkillKey1, baseSkillKey1DouableKeyDownCache);
        codeCachePairs.Add(inputInfo.baseSkillKey2, baseSkillKey2DouableKeyDownCache);
        codeCachePairs.Add(inputInfo.baseSkillKey3, baseSkillKey3DouableKeyDownCache);
        codeCachePairs.Add(inputInfo.baseSkillKey4, baseSkillKey4DouableKeyDownCache);
        codeCachePairs.Add(inputInfo.baseSkillKey5, baseSkillKey5DouableKeyDownCache);
        codeCachePairs.Add(inputInfo.baseSkillKey6, baseSkillKey6DouableKeyDownCache);
        
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

    public bool IsKeyDown(params KeyCodeIndex[] keyCodeIdxs)
    {
        bool result = false;
        if(keyCodeIdxs.Length == 1)
        {
            result = Input.GetKeyDown(GetKeyCode(keyCodeIdxs[0]));
        }
        else
        {
            KeyCode[] keyCodes = GetKeyCodes(keyCodeIdxs);
            result = EqualsKeyCodes(Tool.Sort(keyCodes), Tool.Sort(keysCache.CacheDistinctKeyCodes));
        }
        return result;
    }

    public bool IsKey(params KeyCodeIndex[] keyCodeIdxs)
    {
        bool result = true;
        foreach(KeyCodeIndex tempKCI in keyCodeIdxs)
        {
            result = result && Input.GetKey(GetKeyCode(tempKCI));
            if (!result) break;
        }
        return result;
    }

    public bool IsKeyUp(params KeyCodeIndex[] keyCodeIdxs)
    {
        bool result = true;
        foreach (KeyCodeIndex tempKCI in keyCodeIdxs)
        {
            result = result && Input.GetKeyUp(GetKeyCode(tempKCI));
            if (!result) break;
        }
        return result;
    }

    public bool IsDoubleKeyDown(KeyCodeIndex keyCodeIdx)
    {
        bool result = false;
        KeyCode keyCode = GetKeyCode(keyCodeIdx);
        if (codeCachePairs.ContainsKey(keyCode))
        {
            result = codeCachePairs[keyCode].CacheKeyCodes.Length == 2;
        }
        return result;
    }

    public KeyCode GetKeyCode(KeyCodeIndex keyCodeIdx)
    {
        return allKeyCodes[(int)keyCodeIdx];
    }

    public KeyCode[] GetKeyCodes(params KeyCodeIndex[] keyCodeIdxs)
    {
        KeyCode[] resultKeyCodes = new KeyCode[keyCodeIdxs.Length];
        for (int i = 0; i < resultKeyCodes.Length; i++)
        {
            resultKeyCodes[i] = allKeyCodes[(int)keyCodeIdxs[i]];
        }
        return resultKeyCodes;
    }
}


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

public enum KeyCodeIndex
{
    UpKeyCodeIdx,
    DownKeyCodeIdx,
    LeftKeyCodeIdx,
    RightKeyCodeIdx,
    BaseSkillKeyCodeIdx1,
    BaseSkillKeyCodeIdx2,
    BaseSkillKeyCodeIdx3,
    BaseSkillKeyCodeIdx4,
    BaseSkillKeyCodeIdx5,
    BaseSkillKeyCodeIdx6,
    KeyCodeLength
}
