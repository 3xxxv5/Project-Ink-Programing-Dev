using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;


public class Player : MonoBehaviour
{
    Transform playerTran;
    CharacterController playerController;
    private bool beginCD = false;
    private float CDCount = 1;
    public EnumSpace.PlayStatus moveStatus;


    public Transform characterTran;
    //角色默认移动速度为10
    [Header("默认速度")]
    public float moveSpeed = 10f;
    [Header("冲刺距离")]
    public float dashDis = 20f;
    [Header("冲刺时间")]
    public float lastTime = 0.5f;
    [Header("冲刺CD")]
    public float dashCD = 2f;
    [Header("冲刺曲线")]
    public AnimationCurve curv = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1.0f, 0f, 0f));


    void Start()
    {
        playerTran = this.transform;
        playerController = this.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public float getDashCD()
	{
        return dashCD;
	}

    private void SetStatusToIdle()
	{
        moveStatus = EnumSpace.PlayStatus.Idle;
	}

    void Update()
    {
        PlayerMove();


        //计算cd
        if (beginCD)
        {
            CDCount -= Time.deltaTime / dashCD;
            //Debug.Log(CDCount);
            //Debug.Log(beginCD);
            if (CDCount <= 0)
            {
                beginCD = false;
            }
        }
    }

    void LetMove()
	{
        var camera = Camera.main;
        var screenRay = (camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
        Ray ray = new Ray(screenRay.origin + screenRay.direction * 4.3f, screenRay.direction);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, dashDis))
        {
            //print(hitInfo.transform);
            transform.DOMove(hitInfo.point - ray.direction * 1.0f, lastTime).SetEase(curv);
        }
        else
        {
            transform.DOMove(ray.origin + ray.direction * dashDis, lastTime).SetEase(curv);
        }
        //Debug.Log();
        //Debug.DrawLine(ray.origin, ray.origin + ray.direction * dashDis, Color.red);
        //Debug.Log(Screen.width);
        //transform.DOMove(ray.origin + ray.direction * 15.0f, 1.5f);
    }

    //角色移动
    void PlayerMove()
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
		playerController.Move(playerTran.TransformDirection(new Vector3(moveX, moveY, moveZ)));
        
        //点击鼠标左键加速冲刺
        if (Input.GetMouseButtonDown(0) && beginCD == false)
        {
            //Vector3 localPos = characterTran.localPosition;
            //localPos.z += dashDis;
            //Vector3 pos = playerTran.TransformPoint(localPos);
            //playerTran.DOMove(pos, lastTime);
            //playerTran.DOMove(new Vector3(pos.x, pos.y, pos.z + dashDis), lastTime);
            LetMove();
            beginCD = true;
            CDCount = 1;
            //
            Sequence seq = DOTween.Sequence();
            moveStatus = EnumSpace.PlayStatus.Dash;
            seq.AppendInterval(lastTime);
            seq.AppendCallback(SetStatusToIdle);
        }        
    }
}
