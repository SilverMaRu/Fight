using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHelper
{
    private static Dictionary<string, int> layers = new Dictionary<string, int>();

    public static void Init()
    {
        for(int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);
            if (!string.IsNullOrEmpty(layerName))
            {
                layers.Add(layerName, i);
            }
        }
    }

    /// <summary>
    /// 用layer名获得int值
    /// </summary>
    /// <param name="name">layer名</param>
    /// <returns></returns>
    public static int GetLayer(string name)
    {
        int retLayer = -1;
        if (!layers.TryGetValue(name, out retLayer))
        {
            retLayer = LayerMask.NameToLayer(name);
            layers.Add(name, retLayer);
        }
        return retLayer;
    }

    /// <summary>
    /// 关闭两个layer的碰撞检测
    /// </summary>
    /// <param name="layerName1"></param>
    /// <param name="layerName2"></param>
    public static void OffLayerCollisionMask(string layerName1, string layerName2)
    {
        OffLayerCollisionMask(GetLayer(layerName1), GetLayer(layerName2));
    }

    /// <summary>
    /// 关闭两个layer的碰撞检测
    /// </summary>
    /// <param name="layer1"></param>
    /// <param name="layer2"></param>
    public static void OffLayerCollisionMask(int layer1, int layer2)
    {
        //Physics2D.SetLayerCollisionMask(layer1, 0 << layer2);
        Physics2D.SetLayerCollisionMask(layer1, GetLayerMask(layer2, false));
    }

    /// <summary>
    /// 打开两个layer的碰撞检测
    /// </summary>
    /// <param name="layerName1"></param>
    /// <param name="layerName2"></param>
    public static void OnLayerCollisionMask(string layerName1, string layerName2)
    {
        OnLayerCollisionMask(GetLayer(layerName1), GetLayer(layerName2));
    }

    /// <summary>
    /// 打开两个layer的碰撞检测
    /// </summary>
    /// <param name="layer1"></param>
    /// <param name="layer2"></param>
    public static void OnLayerCollisionMask(int layer1, int layer2)
    {
        //Physics2D.SetLayerCollisionMask(layer1, 1 << layer2);
        Physics2D.SetLayerCollisionMask(layer1, GetLayerMask(layer2, true));
    }

    public static int GetLayerMask(string layerName, bool on)
    {
        return GetLayerMask(GetLayer(layerName), on);
    }

    public static int GetLayerMask(int layer, bool on)
    {
        // 位移值
        int d = on ? 1 : 0;
        return d << layer;
    }

    public static int GetLayerMask(string[] layerNames, bool on)
    {
        int[] layers = new int[layerNames.Length];
        for (int i = 0; i < layerNames.Length; i++)
        {
            layers[i] = GetLayer(layerNames[i]);
        }
        return GetLayerMask(layers, on);
    }

    public static int GetLayerMask(int[] layers, bool on)
    {
        int layerMask = 0;
        // 位移值
        int d = on ? 1 : 0;
        foreach (int tempLayer in layers)
        {
            layerMask = layerMask | (d << tempLayer);
        }
        return layerMask;
    }
}
