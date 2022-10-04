using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Vector2 _mousePos;
    private float lookAngle;
    void Update()
    {
        //Find mouse and point to it
        _mousePos = Input.mousePosition;
        lookAngle = 90 + Mathf.Atan2(_mousePos.x, _mousePos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }
}
