using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrane : BasePlayer
{
    private float timer = 0;
    [Header("蓄力阈值")]
    public float threshold = 0.2f;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveStatus != EnumSpace.PlayStatus.Faint)
        {
            PlayerMove();
        }
        CheckIsInCD();
        CheckBulletTime();
    }

    protected override void CheckDash()
    {
        //按住鼠标timer开始计时
        if (Input.GetMouseButton(0) && beginCD == false)
        {
            timer += Time.deltaTime;
            if (timer > threshold && moveStatus != EnumSpace.PlayStatus.Charge)
            {
                characterGO.GetComponent<CraneAnimator>().Charge();
                SetMoveStatus(EnumSpace.PlayStatus.Charge);
            }
        }

        if (Input.GetMouseButtonUp(0) && beginCD == false)
        {
            beginCD = true;
            cd = 1;
            characterGO.GetComponent<CraneAnimator>().Launch();
            SetMoveStatus(EnumSpace.PlayStatus.Launch);
            timer = 0;
        }
    }

    protected void CheckBulletTime()
    {

    }

    protected override void PlayWalkAnim()
    {
        characterGO.GetComponent<CraneAnimator>().Walk();
    }

    protected override void PlayIdleAnim()
    {
        characterGO.GetComponent<CraneAnimator>().Idle();
    }
}
