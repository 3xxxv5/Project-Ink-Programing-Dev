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
        public float waitTime;
        public Vector3 speed;
    }

    public GameObject player;
    public GameObject guide;
    public Transform cam;
    public GameObject front;
    public GameObject cooling;
    public Path[] path = new Path[0];
    private int id;
    

    void Awake()
    {
        cam.position = path[0].point.position;
        front.SetActive(false);
        cooling.SetActive(false);
        player.GetComponent<PlayerShrimp>().enabled = false;
        player.GetComponent<ThirdPersonCamera>().enabled = false;
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
                cam.position += p.speed * Time.deltaTime;
            }
            else
            {
                cam.position = p.point.position;
                id++;
                if(id == path.Length)
                {
                    //if (GameMesMananger.Instance().save.isLevelPass[1] == false)
                    //{
                    //    guide.SetActive(true);
                    //}
                    guide.SetActive(true);
                    player.SetActive(true);
                    player.GetComponent<PlayerShrimp>().enabled = true;
                    //player.GetComponent<ThirdPersonCamera>().enabled = true;
                    front.SetActive(true);
                    cooling.SetActive(true);
                    //销毁自身
                    Destroy(cam.gameObject);
                    Destroy(this);
                }
            }
        }
    }
}
