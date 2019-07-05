using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public bool lookAtOtherPlayer = true;
    public Transform targetTrans;

    private Transform myTrans;

    // Start is called before the first frame update
    void Start()
    {
        myTrans = transform;
        if (lookAtOtherPlayer)
        {
            GameObject otherPlayerGO = FindOtherPlayer();
            if (otherPlayerGO != null)
            {
                targetTrans = otherPlayerGO.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookAtEnemy(targetTrans);
    }

    private GameObject FindOtherPlayer()
    {
        GameObject[] playerGOs = GameObject.FindGameObjectsWithTag("Player");
        GameObject resultGO = null;
        foreach (GameObject tempGO in playerGOs)
        {
            if (!tempGO.Equals(gameObject))
            {
                resultGO = tempGO;
                break;
            }
        }
        return resultGO;
    }

    private void LookAtEnemy(Transform enemyTrans)
    {
        if (enemyTrans != null)
        {
            Vector3 myToEnemyNormalized = enemyTrans.position - myTrans.position;
            myToEnemyNormalized = Vector3.Scale(myToEnemyNormalized, Vector3.right).normalized;
            myTrans.right = myToEnemyNormalized.normalized;
        }
    }
}
