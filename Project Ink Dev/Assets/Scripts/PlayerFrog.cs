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
        IsInCD();
    }

    protected override void MouseClick()
    {
        //按住鼠标timer开始计时
        if(Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            //按住鼠标时间超过阈值判定为蓄力，播放蓄力动画
            if (timer > threshold && moveStatus == EnumSpace.PlayStatus.Idle)
            {
                characterGO.GetComponent<FrogAnimator>().Charge();
                moveStatus = EnumSpace.PlayStatus.Charge;
            }
        }

        //抬起鼠标发射
        if(Input.GetMouseButtonUp(0))
        {
            characterGO.GetComponent<FrogAnimator>().Launch();
            moveStatus = EnumSpace.PlayStatus.Idle;
            timer = 0;
        }
    }
}
