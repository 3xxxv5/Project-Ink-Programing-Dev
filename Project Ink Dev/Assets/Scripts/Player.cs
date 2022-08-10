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

    bool isWalk, isIdle, isJump;

    void Start()
    {
        playerTran = this.transform;
        playerController = this.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        PlayerMove();
        if (isWalk)
        {
            playWalkAniamtion();
        }
        if(isIdle)
		{
            playIdleAniamtion();
		}


        //计算cd
        if (beginCD)
        {
            CDCount -= Time.deltaTime / dashCD;
            Debug.Log(CDCount);
            Debug.Log(beginCD);
            if (CDCount <= 0)
            {
                beginCD = false;
            }
        }
    }

    void playJumpAnimation()
	{
        var playerAnimation = transform.Find("Shrimp").GetComponent<PlayerAnimation>();
        //playerAnimation.PlayerStatusChange(transform.eulerAngles, true, "jump");
	}

    void playWalkAniamtion()
	{
        var playerAnimation = transform.Find("Shrimp").GetComponent<PlayerAnimation>();
        //playerAnimation.PlayerStatusChange(transform.eulerAngles, true, "jump");
    }

    void playIdleAniamtion()
    {
        var playerAnimation = transform.Find("Shrimp").GetComponent<PlayerAnimation>();
       //playerAnimation.PlayerStatusChange(transform.eulerAngles, true, "idle");
    }

    //角色移动
    void PlayerMove()
    {
        float moveX = 0, moveY = 0, moveZ = 0;

        if (!isJump)
        {
            //前后移动
            if (Input.GetKey(KeyCode.W))
            {
                isWalk = true;
                isIdle = false;
                isJump = false;
                moveZ += moveSpeed * Time.deltaTime;
            }
            if(Input.GetKeyUp(KeyCode.W))
			{
                isWalk = false;
                isIdle = true;
			}
            //else if (Input.GetKey(KeyCode.S))
            //{
            //    moveZ -= moveSpeed * Time.deltaTime;
            //}
        }
        ////左右移动
        //if (Input.GetKey(KeyCode.A))
        //{
        //    moveX -= moveSpeed * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    moveX += moveSpeed * Time.deltaTime;
        //}
        playerController.Move(playerTran.TransformDirection(new Vector3(moveX, moveY, moveZ)));
        
        //点击鼠标左键加速冲刺
        if (Input.GetMouseButtonDown(0) && beginCD == false)
        {
            Vector3 localPos = characterTran.localPosition;
            localPos.z += dashDis;
            Vector3 pos = playerTran.TransformPoint(localPos);
            playJumpAnimation();
            playerTran.DOMove(pos, lastTime);
            //playerTran.DOMove(new Vector3(pos.x, pos.y, pos.z + dashDis), lastTime);
            beginCD = true;
            CDCount = 1;

        }        
    }
}
