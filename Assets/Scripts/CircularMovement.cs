using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public Transform rotation_center;
    public float AngularSpeed, RotationRadius;

    private float PosX, PosY, angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle = angle + Time.deltaTime * AngularSpeed;

        //add astroid startposition
        PosX = rotation_center.position.x + Mathf.Cos(angle) * RotationRadius;
        PosY = rotation_center.position.y + Mathf.Sin(angle) * RotationRadius;

        transform.position = new Vector2(PosX, PosY);
        
        if(angle >= 360)
        {
            angle = 0;
        }
    
    }
}
