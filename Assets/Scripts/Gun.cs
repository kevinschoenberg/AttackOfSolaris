using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class Gun : MonoBehaviour
{

    public Camera cam;
    public Transform pos;

    private Vector2 _mousePos;
    private Vector2 _playerPos;
    public float gunRotion;
    SpriteRenderer sprite;

    public float angle;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //Find mouse and point to it
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDirection = _mousePos - new Vector2(pos.position.x, pos.position.y);
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        pos.rotation = Quaternion.Euler(0f, 0f, angle);
        if (angle > 90 || angle < -90)
        {
            sprite.flipY = true;
        }
        else
        {
            sprite.flipY = false;
        }
    }
}
