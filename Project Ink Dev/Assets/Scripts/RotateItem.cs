﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : PowerFlower1
{
    //Rigidbody rb;
    Transform ts;
    [Header("旋转轴相对物体位置")]
    public Vector3 pivot = new Vector3(2, 0, 0);
    [Header("旋转速度")]
    public float rotateSpeed = 3;
    
    void OnEnable()
    {
        init();
        rb = GetComponent<Rigidbody>();
        ts = GetComponent<Transform>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0f, -m_velocity, 0f);
        pivot += ts.position;
    }

    public override void SetFallingTrack()
    {
        var m = rb.mass;
        transform.RotateAround(pivot, Vector3.down, rotateSpeed);
        Vector3 force = new Vector3(0f, -m * m_acceleratetion, 0f);
        rb.AddForce(force);
    }
}
