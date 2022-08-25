using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartMenuCameraMove : MonoBehaviour
{
    public float MOVE_SPEED = 1.0f;
    public float moveX = 100.0f;
    //public float moveNX = 1.0f;
    // Start is called before the first frame update
    private float startPosX;
    bool right = true;
    void Start()
    {
        startPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (right) {
            transform.position = new Vector3(transform.position.x + MOVE_SPEED, transform.position.y, transform.position.z);
            if (transform.position.x > startPosX + moveX)
                right = false;
        }
		else
		{
            transform.position = new Vector3(transform.position.x - MOVE_SPEED, transform.position.y, transform.position.z);
            if (transform.position.x < startPosX - moveX)
                right = true;
        }

    }
}
