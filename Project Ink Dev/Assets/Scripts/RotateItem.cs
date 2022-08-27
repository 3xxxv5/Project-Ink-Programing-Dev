using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

public class RotateItem : PowerFlower1
{
    Rigidbody rb1;
    Transform ts;
    [Header("旋转轴相对物体位置")]
    public Vector3 pivot = new Vector3(2, 0, 0);
    [Header("旋转速度")]
    public float rotateSpeed = 3;
    public float M_REBIRTHTIME;
    private float rotationRebirthTimeTemp;


    void Start()
    {
        init();
        rb1 = GetComponent<Rigidbody>();
        ts = GetComponent<Transform>();
        rb1.useGravity = false;
        rb1.velocity = new Vector3(0f, -m_velocity, 0f);
        pivot += transform.position;
        rotationRebirthTimeTemp = m_rebirthTime;
    }

    public override void SetFallingTrack()
    {
        var m = rb1.mass;
        transform.RotateAround(pivot, Vector3.down, rotateSpeed);
        Vector3 force = new Vector3(0f, -m * m_acceleratetion, 0f);
        rb1.AddForce(force);
    }

    public override void DropRebirth()
    {
        if (m_rebirthTime < 0.0f)
        {
            

            switch (itemType)
            {
                case ItemType.Main:
                    //Debug.Log("Rebirth");
                    transform.position = m_originalPosition;
                    m_rebirthTime = rotationRebirthTimeTemp;
                    //Debug.Log(m_rebirthTime);
                    break;
                case ItemType.Hidden:
                    //Debug.Log("Destroy");
                    Destroy(this.gameObject);
                    break;
            }
        }

        m_rebirthTime -= Time.deltaTime;
    }

 //   private void FixedUpdate()
	//{
 //       SetFallingTrack();
        
 //   }

    void Update()
    {
        DropRebirth();
    }
}
