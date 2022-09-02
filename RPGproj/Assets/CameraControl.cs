using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    float HWidth;
    float HHeight;
    float MinX, MaxX, MinY, MaxY;
    void Start()
    {
        HWidth = Camera.main.aspect * Camera.main.orthographicSize;
        HHeight = Camera.main.orthographicSize;
        MinX = -1; MinY = -5;
        MaxX = 51; MaxY = 10;
    }


    void Update()
    {
        Vector3 LimitPos = new Vector3(
            Mathf.Clamp(Player.transform.position.x, MinX + HWidth, MaxX - HWidth), 
            Mathf.Clamp(Player.transform.position.y, MinY + HHeight, MaxY - HHeight),
            -10);
        transform.position = Vector3.Lerp(transform.position, LimitPos, Time.deltaTime * 4.0f);
    }
}
