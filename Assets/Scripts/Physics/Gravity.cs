using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityScale = 1;
    public string[] standLayerNames = new string[1] { "Ground" };

    private Vector2 gravity = Vector2.zero;
    private int standLayerMask = 0;
    private Transform myTrans;
    private Collider2D coll2D;
    private float uncheckStandStartTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        gravity = Physics2D.gravity * gravityScale;
        standLayerMask = LayerHelper.GetLayerMask(standLayerNames, true);
        myTrans = transform;
        coll2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = coll2D.bounds;
        Vector3 extents = bounds.extents;
        RaycastHit2D[] hit2Ds = Tool.RaycastAll2D(bounds.center, -myTrans.up, extents.y, extents.x, 2, LayerHelper.GetLayerMask(standLayerNames, true));
        bool checkStand = Tool.IsCheck(hit2Ds);
        if (!checkStand)
        {
            if (uncheckStandStartTime < 0)
            {
                uncheckStandStartTime = Time.time;
            }
            float deltaTime = Time.time - uncheckStandStartTime;
            myTrans.position += (Vector3)(gravity * deltaTime * deltaTime * .5f);
        }
        else
        {
            Vector2 point = AveragePoint(hit2Ds);
            Vector3 position = myTrans.position;
            float x = position.x;
            float y = point.y + extents.y - (bounds.center.y - position.y);
            float z = position.z;
            Vector3 newPosition = Vector3.right * x + Vector3.up * y + Vector3.forward * z;
            myTrans.position = newPosition;
            uncheckStandStartTime = -1;
        }
    }

    //private void FixedUpdate()
    //{
    //    //Bounds bounds = coll2D.bounds;
    //    //Vector3 extents = bounds.extents;
    //    //RaycastHit2D[] hit2Ds = Tool.RaycastAll(bounds.center, -myTrans.up, extents.y, extents.x, 2, LayerHelper.GetLayerMask(standLayerNames, true));
    //    //bool checkStand = IsCheck(hit2Ds);
    //    //if (!checkStand)
    //    //{
    //    //    if (uncheckStandStartTime < 0)
    //    //    {
    //    //        uncheckStandStartTime = Time.fixedTime;
    //    //    }
    //    //    float deltaTime = Time.fixedTime - uncheckStandStartTime;
    //    //    myTrans.position += (Vector3)(gravity * deltaTime * deltaTime * .5f);
    //    //}
    //    //else
    //    //{
    //    //    Vector2 point = AveragePoint(hit2Ds);
    //    //    Vector3 position = myTrans.position;
    //    //    float x = position.x;
    //    //    float y = point.y + extents.y - (bounds.center.y - position.y);
    //    //    float z = position.z;
    //    //    Vector3 newPosition = Vector3.right * x + Vector3.up * y + Vector3.forward * z;
    //    //    myTrans.position = newPosition;
    //    //    uncheckStandStartTime = -1;
    //    //}
    //}

    private Vector2 AveragePoint(RaycastHit2D[] hit2Ds)
    {
        Vector2 resultVector2 = Vector2.zero;
        int effectiveNum = 0;
        foreach(RaycastHit2D tempHit2D in hit2Ds)
        {
            if(tempHit2D.transform != null)
            {
                effectiveNum++;
                resultVector2 += tempHit2D.point;
            }
        }
        resultVector2 /= effectiveNum;
        return resultVector2;
    }
}
