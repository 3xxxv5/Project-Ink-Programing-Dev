using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrog : BasePlayer
{
    private float timer = 0;

    [Header("蓄力阈值")]
    public float threshold = 0.2f;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (moveStatus != EnumSpace.PlayStatus.Faint)
        {
            PlayerMove();
        }
        //计算cd
        CheckIsInCD();
    }

    protected override void MouseClick()
    {
        //按住鼠标timer开始计时
        if(Input.GetMouseButton(0) && beginCD == false)
        {
            timer += Time.deltaTime;
            
            //按住鼠标时间超过阈值判定为蓄力，播放蓄力动画
            if (timer > threshold && moveStatus == EnumSpace.PlayStatus.Idle)
            {
                //toDo
                characterGO.GetComponent<FrogAnimator>().Charge();
                SetMoveStatus(EnumSpace.PlayStatus.Charge);
            }
        }

        //抬起鼠标发射
        if(Input.GetMouseButtonUp(0) && beginCD == false)
        {
            //判断条件不同，抬起鼠标才开始计算cd
            beginCD = true;
            cd = 1;

            characterGO.GetComponent<FrogAnimator>().Launch();
            //ChangeMoveStatus(EnumSpace.PlayStatus.Idle);
            timer = 0;
        }
    }

    protected override void PlayWalkAnim()
    {
        characterGO.GetComponent<FrogAnimator>().Walk();
    }

    protected override void PlayIdleAnim()
    {
        characterGO.GetComponent<FrogAnimator>().Idle();
    }
}
