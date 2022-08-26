using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuideCameraMove : MonoBehaviour
{
    [Serializable]
    public class Path
    {
        public Transform point;
        public float moveTime;
        public Vector3 speed;
    }
    
    public Path[] path = new Path[0];
    private int id;
    public Transform target;

    void Start()
    {
        target.position = path[0].point.position;
        //计算两个点之间的速度
        for (int i = 1; i < path.Length; i++)
        {
            path[i].speed = (path[i].point.position - path[i - 1].point.position) / path[i].moveTime;
        }
    }

    void Update()
    {
        if (id < path.Length)
        {
            Path p = path[id];
            if(p.moveTime > 0)
            {
                p.moveTime -= Time.deltaTime;
                target.position += p.speed * Time.deltaTime;
            }
            else
            {
                target.position = p.point.position;
                id++;
            }
        }
    }
}
