using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FlowMode
{
    LeftToRight,
    RightToLeft,
    TopToBottom,
    BottomToTop
}

public class UpdateStrip : MonoBehaviour
{
    public Color maxColor;
    public Color middleColor;
    public Color minColor;
    public FlowMode flowMode = FlowMode.LeftToRight;
    public float changeTime = 0.5f;

    private float lastCurrent = float.MaxValue;
    private RectTransform currentPanelRectTrans;
    private Image fillImage;
    private Coroutine smoothRectTrans;
    private Coroutine smoothColor;

    // Start is called before the first frame update
    void Start()
    {
        currentPanelRectTrans = (RectTransform)transform.Find("CurrentPanel");
        fillImage = currentPanelRectTrans.Find("FillImage").GetComponentInChildren<Image>();
    }

    public void OnChange(float current, float max)
    {
        float normalized = Mathf.Clamp(current / max, 0, 1);
        UpdateRectTrans(normalized, current);
        UpdateColor(normalized);
    }

    private void UpdateRectTrans(float normalized, float current)
    {
        Vector2 targetAnchorMin = currentPanelRectTrans.anchorMin;
        Vector2 targetAnchorMax = currentPanelRectTrans.anchorMax;
        switch (flowMode)
        {
            case FlowMode.LeftToRight:
                targetAnchorMin = (1 - normalized) * Vector2.right + currentPanelRectTrans.anchorMin.y * Vector2.up;
                break;
            case FlowMode.RightToLeft:
                targetAnchorMax = normalized * Vector2.right + currentPanelRectTrans.anchorMax.y * Vector2.up;
                break;
            case FlowMode.TopToBottom:
                targetAnchorMax = currentPanelRectTrans.anchorMax.x * Vector2.right + normalized * Vector2.up;
                break;
            case FlowMode.BottomToTop:
                targetAnchorMin = currentPanelRectTrans.anchorMin.x * Vector2.right + (1 - normalized) * Vector2.up;
                break;
        }
        if (smoothRectTrans != null) StopCoroutine(smoothRectTrans);
        if (lastCurrent > current)
        {
            smoothRectTrans = StartCoroutine(SmoothRectTrans(currentPanelRectTrans.anchorMin, targetAnchorMin, currentPanelRectTrans.anchorMax, targetAnchorMax));
        }
        else
        {
            currentPanelRectTrans.anchorMin = targetAnchorMin;
            currentPanelRectTrans.anchorMax = targetAnchorMax;
        }
        lastCurrent = current;
    }

    private void UpdateColor(float normalized)
    {
        Color currentColor = maxColor;
        if (normalized > 0 && normalized <= 0.5f)
        {
            normalized *= 2;
            currentColor = Color.Lerp(minColor, middleColor, normalized);
        }
        else if (normalized > 0.5f && normalized < 1)
        {
            currentColor = Color.Lerp(middleColor, maxColor, normalized);
        }
        if (smoothColor != null) StopCoroutine(smoothColor);
        smoothColor = StartCoroutine(SmoothColor(fillImage.color, currentColor));
    }

    private IEnumerator SmoothRectTrans(Vector2 startMinAnchor, Vector2 targetMinAnchor, Vector2 startMaxAnchor, Vector2 targetMaxAnchor)
    {
        float usedTime = 0;
        while (usedTime <= changeTime)
        {
            usedTime += Time.deltaTime;
            float t = usedTime / changeTime;
            currentPanelRectTrans.anchorMin = Vector2.Lerp(startMinAnchor, targetMinAnchor, t);
            currentPanelRectTrans.anchorMax = Vector2.Lerp(startMaxAnchor, targetMaxAnchor, t);
            yield return null;
        }
    }

    private IEnumerator SmoothColor(Color startColor, Color targetColor)
    {
        float usedTime = 0;
        while (usedTime <= changeTime)
        {
            usedTime += Time.deltaTime;
            float t = usedTime / changeTime;
            fillImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
    }
}
