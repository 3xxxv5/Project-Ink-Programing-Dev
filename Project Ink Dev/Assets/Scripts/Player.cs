using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Transform playerTran;
    CharacterController playerController;
    Transform cameraTran;
    //摄像机旋转角度
    Vector3 cameraRotation;
    UnityEvent leftClick;

    //角色默认移动速度为10
    [Header("默认速度")]
    public float moveSpeed = 10f;
    [Header("冲刺速度")]
    public float speedUp = 100f;
    [Header("冲刺时间")]
    public float lastTime = 0.5f;
    //相机相对角色高度
    [Header("相机相对角色高度")]
    public float cameraHeight = 0.5f;
    //相机相对角色距离
    [Header("相机与角色距离")]
    public float cameraDis = 2f;

    void Start()
    {
        playerTran = this.transform;
        playerController = this.GetComponent<CharacterController>();

        //获取主相机
        cameraTran = Camera.main.transform;
        Vector3 pos = playerTran.position;
        pos.y += cameraHeight;
        cameraTran.position = pos;

        //相机旋转方向与角色一致
        cameraTran.rotation = playerTran.rotation;
        cameraRotation = cameraTran.eulerAngles;

        //锁定鼠标,不显示鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //leftClick.AddListener(new UnityAction(ButtonLeftClick));
    }

    void Update()
    {
        Control();
    }

    void Control()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //旋转相机

        cameraRotation.x -= mouseY;
        cameraRotation.y += mouseX;
        cameraTran.eulerAngles = cameraRotation;

        //角色面向方向与相机一致
        Vector3 camRot = cameraTran.eulerAngles;
        //camRot.x = 0;camRot.z = 0;
        playerTran.eulerAngles = camRot;

        playerMove();

        //相机位置跟随角色
        Vector3 camPos = playerTran.position;
        camPos.y += cameraHeight;
        camPos.z -= cameraDis;
        cameraTran.position = camPos;
    }

    //角色移动
    void playerMove()
    {
        float moveX = 0, moveY = 0, moveZ = 0;

        //前后移动
        if (Input.GetKey(KeyCode.W))
        {
            moveZ += moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveZ -= moveSpeed * Time.deltaTime;
        }

        //左右移动
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX += moveSpeed * Time.deltaTime;
        }
        //test
        //点击鼠标左键加速冲刺
        if (Input.GetMouseButtonDown(0))
        {
            moveZ += speedUp * lastTime;
        }
        //if(PointerEventData.InputButton.Left)
        playerController.Move(playerTran.TransformDirection(new Vector3(moveX, moveY, moveZ)));
    }
}
