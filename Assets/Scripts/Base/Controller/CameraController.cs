using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float minSize;
    public bool inspectorSizeIsMinSize = true;
    public float maxSize;
    public Transform borderLeftTrans;
    public Transform borderRightTrans;

    private float currentSize;
    private Transform[] playerTranss;

    // Start is called before the first frame update
    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        if (inspectorSizeIsMinSize)
        {
            minSize = mainCamera.orthographicSize;
        }
        GameObject[] playerGOs = GameObject.FindGameObjectsWithTag("Player");
        if(playerGOs != null)
        {
            playerTranss = new Transform[playerGOs.Length];
            for(int i = 0; i < playerGOs.Length; i++)
            {
                playerTranss[i] = playerGOs[i].transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetSize();
        ResetPosition();
    }

    private void ResetSize()
    {
        if (playerTranss.Length < 2) return;
        float distance = Mathf.Abs(playerTranss[0].position.x - playerTranss[1].position.x) + 1;
        currentSize = Tool.GetCameraOrthographicSizeByWidth(distance);
        currentSize = Mathf.Clamp(currentSize, minSize, maxSize);
        mainCamera.orthographicSize = currentSize;
    }

    private void ResetPosition()
    {
        if (playerTranss.Length < 2) return;
        float centerX = (playerTranss[0].position.x + playerTranss[1].position.x) * .5f;
        float cameraWidth = Tool.GetCameraScale().x;
        float halfCameraWidth = cameraWidth * .5f;
        float borderLeftX = borderLeftTrans.position.x;
        float borderRightX = borderRightTrans.position.x;
        float leghtLeftToRight = borderRightX - borderLeftX;
        if (leghtLeftToRight >= cameraWidth && centerX - halfCameraWidth < borderLeftX)
        {
            centerX = borderLeftX + halfCameraWidth;
        }
        else if (leghtLeftToRight >= cameraWidth && centerX + halfCameraWidth > borderRightX)
        {
            centerX = borderRightX - halfCameraWidth;
        }
        Vector3 newPosition = Vector3.right * centerX + Vector3.up * mainCamera.transform.position.y + Vector3.forward * mainCamera.transform.position.z;
        mainCamera.transform.position = newPosition;
    }
}
