using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//角色控制器基类
public abstract class BasePlayer : MonoBehaviour
{
    protected Transform playerTran;
    protected CharacterController playerController;
    protected bool beginCD = false;
    protected float cd = 1;
    protected bool isMouseButtonUp = false;

    public GameObject characterGO;
    public Image coolingFull;
    //角色默认移动速度为10
    [Header("默认速度")]
    public float MOVE_SPEED = 10f;
    [Header("冲刺距离")]
    public float DASH_DIS = 20f;
    [Header("冲刺时间")]
    public float LAST_TIME = 2.0f;
    [Header("冲刺CD")]
    public float dashCD = 2f;
    [Header("冲刺曲线")]
    public AnimationCurve curv = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1.0f, 0f, 0f));

    //所有初始化时调用
    protected void Init()
    {
        playerTran = this.transform;
        playerController = this.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerStatusManager.Instance().SetMoveStatus(EnumSpace.PlayStatus.Idle);
    }





    public void CheckIsInCD()
    {
        if (beginCD)
        {
            cd -= Time.deltaTime / dashCD;
            coolingFull.fillAmount = 1 - cd;
            if (cd <= 0)
            {
                beginCD = false;
            }
        }
    }

    //射线检测
    protected void LetMove()
    {
        var camera = Camera.main;
        var screenRay = (camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
        Ray ray = new Ray(screenRay.origin + screenRay.direction * 4.3f, screenRay.direction);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, DASH_DIS))
        {
            //print(hitInfo.transform);
            transform.DOMove(hitInfo.point - ray.direction * 1.0f, LAST_TIME).SetEase(curv);
        }
        else
        {
            transform.DOMove(ray.origin + ray.direction * DASH_DIS, LAST_TIME).SetEase(curv);
        }
    }

    //抽象方法，鼠标点击触发冲刺
    protected abstract void CheckDash();
    //播放walk动画
    protected abstract void PlayWalkAnim();
    //
    protected abstract void PlayIdleAnim();

    //角色移动
    protected void PlayerMove()
    {
        float moveX = 0, moveY = 0, moveZ = 0;

        if(CanMove())
        {
            //前后移动
            if (Input.GetKey(KeyCode.W))
            {
                moveZ += MOVE_SPEED * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveZ -= MOVE_SPEED * Time.deltaTime;
            }
            //左右移动
            if (Input.GetKey(KeyCode.A))
            {
                moveX -= MOVE_SPEED * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveX += MOVE_SPEED * Time.deltaTime;
            }
        }

        if (moveX != 0 || moveZ != 0)
        {
            if (PlayerStatusManager.Instance().GetPlayerMoveStatus() != EnumSpace.PlayStatus.Walk)
            {
                PlayerStatusManager.Instance().SetMoveStatus(EnumSpace.PlayStatus.Walk);
                PlayWalkAnim();
            }
        }
        else
        {
            if (PlayerStatusManager.Instance().GetPlayerMoveStatus() == EnumSpace.PlayStatus.Walk)
            {
                PlayIdleAnim();
                PlayerStatusManager.Instance().SetMoveStatus(EnumSpace.PlayStatus.Idle);
            }
        }

        playerController.Move(playerTran.TransformDirection(new Vector3(moveX, moveY, moveZ)));

        CheckDash();
    }

    protected bool CanMove()
    {
        EnumSpace.PlayStatus status = PlayerStatusManager.Instance().GetPlayerMoveStatus();
        if (status != EnumSpace.PlayStatus.Dash && status != EnumSpace.PlayStatus.Charge && status != EnumSpace.PlayStatus.Launch)
        {
            return true;
        }
        return false;
    }
}
