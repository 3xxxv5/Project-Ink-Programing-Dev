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
    //�������ת�Ƕ�
    Vector3 cameraRotation;
    UnityEvent leftClick;

    //��ɫĬ���ƶ��ٶ�Ϊ10
    [Header("Ĭ���ٶ�")]
    public float moveSpeed = 10f;
    [Header("����ٶ�")]
    public float speedUp = 100f;
    [Header("���ʱ��")]
    public float lastTime = 0.5f;
    //�����Խ�ɫ�߶�
    [Header("�����Խ�ɫ�߶�")]
    public float cameraHeight = 0.5f;
    //�����Խ�ɫ����
    [Header("������ɫ����")]
    public float cameraDis = 2f;

    void Start()
    {
        playerTran = this.transform;
        playerController = this.GetComponent<CharacterController>();

        //��ȡ�����
        cameraTran = Camera.main.transform;
        Vector3 pos = playerTran.position;
        pos.y += cameraHeight;
        cameraTran.position = pos;

        //�����ת�������ɫһ��
        cameraTran.rotation = playerTran.rotation;
        cameraRotation = cameraTran.eulerAngles;

        //�������,����ʾ�����
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
        //��ת���

        cameraRotation.x -= mouseY;
        cameraRotation.y += mouseX;
        cameraTran.eulerAngles = cameraRotation;

        //��ɫ�����������һ��
        Vector3 camRot = cameraTran.eulerAngles;
        //camRot.x = 0;camRot.z = 0;
        playerTran.eulerAngles = camRot;

        playerMove();

        //���λ�ø����ɫ
        Vector3 camPos = playerTran.position;
        camPos.y += cameraHeight;
        camPos.z -= cameraDis;
        cameraTran.position = camPos;
    }

    //��ɫ�ƶ�
    void playerMove()
    {
        float moveX = 0, moveY = 0, moveZ = 0;

        //ǰ���ƶ�
        if (Input.GetKey(KeyCode.W))
        {
            moveZ += moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveZ -= moveSpeed * Time.deltaTime;
        }

        //�����ƶ�
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX += moveSpeed * Time.deltaTime;
        }
        //test
        //������������ٳ��
        if (Input.GetMouseButtonDown(0))
        {
            moveZ += speedUp * lastTime;
        }
        //if(PointerEventData.InputButton.Left)
        playerController.Move(playerTran.TransformDirection(new Vector3(moveX, moveY, moveZ)));
    }
}
