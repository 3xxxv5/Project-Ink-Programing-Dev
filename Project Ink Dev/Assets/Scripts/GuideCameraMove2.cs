using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class GuideCameraMove2 : MonoBehaviour
{
    [Serializable]
    public class Path
    {
        public Transform point;
        public float moveTime;
        public float waitTime;
        public Vector3 speed;
        //public float rotateSpeed;
    }

    public GameObject player;
    public GameObject guide;
    public Transform cam;
    public GameObject front;
    public GameObject cooling;
    public GameObject storage;
    public Path[] path = new Path[0];
    private int id = 0;


    void Awake()
    {
        //stage = GameMesMananger.Instance().getCurStageNum();
        cam.position = path[0].point.position;
        cam.LookAt(path[id + 1].point.position);
        front.SetActive(false);
        cooling.SetActive(false);
        storage.SetActive(false);
        player.GetComponent<PlayerFrog>().enabled = false;
        player.GetComponent<ThirdPersonCamera>().enabled = false;
        //计算两个点之间的速度、旋转角度
        for (int i = 1; i < path.Length; i++)
        {
            path[i].speed = (path[i].point.position - path[i - 1].point.position) / path[i].moveTime;
            //path[i].rotateSpeed = Vector3.Angle(path[i - 1].point.position, path[i].point.position) / path[i].moveTime;
        }
    }


    void Update()
    {
        if (id < path.Length)
        {
            Path p = path[id];
            if (p.moveTime > 0)
            {
                cam.position = p.point.position;
                p.moveTime -= Time.deltaTime;
                //cam.position += p.speed * Time.deltaTime;
                cam.LookAt(path[id + 1].point.position);
                //Vector3 target = new Vector3(p.point.position.x, 0, p.point.position.z);
                //float angle = Vector3.Angle(cam.forward, target);
                //cam.Rotate(cam.up * angle);
                //cam.Rotate(new Vector3(0, p.rotateSpeed, 0), Space.Self);
                //cam.rotation = Quaternion.LookRotation(Vector3.RotateTowards(cam.forward, p.point.position - cam.position, p.rotateSpeed * Time.deltaTime, 0.0f));
            }
            else
            {
                //cam.position = p.point.position;
                id += 2;
                if (id >= path.Length)
                {
                    //if (GameMesMananger.Instance().save.isLevelPass[2] == false)
                    //{
                    //    guide.SetActive(true);
                    //}
                    
                    //player.SetActive(true);

                    player.GetComponent<PlayerFrog>().enabled = true;
                    player.GetComponent<ThirdPersonCamera>().enabled = true;
                    guide.SetActive(true);
                    Guide2MesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;

                    front.SetActive(true);
                    cooling.SetActive(true);
                    storage.SetActive(true);

                    //销毁自身
                    DOTween.Clear(true);
                    Destroy(cam.gameObject);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
