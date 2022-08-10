using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

public class PowerFlower_1 : Item
{

    Rigidbody rb;

    protected override void OnTriggerEnter(Collider other)
    {
        DefaultOnTriggerEnterImplement(other);
    }

    public override void SetFallingTrack()
    {
        var m = rb.mass;
        Vector3 force = new Vector3(0f,-m*m_acceleratetion,0f);
        rb.AddForce(force);
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f,-m_velocity,0f);
    }

    void Update()
    {
        SetFallingTrack();
    }
}