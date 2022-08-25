using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrane : BasePlayer
{
    private float timer = 0;
    EnumSpace.BulletTimeStatus bulletTimeStatus = EnumSpace.BulletTimeStatus.OUT;
    [Header("蓄力阈值")]
    public float threshold = 0.2f;
    [Header("子弹时间持续时间")]
    public float BULLET_TIME_DURATION = 3f;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStatusManager.Instance().GetPlayerMoveStatus() != EnumSpace.PlayStatus.Faint)
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
            timer += Time.unscaledDeltaTime;
            if (timer > threshold && PlayerStatusManager.Instance().GetPlayerMoveStatus() != EnumSpace.PlayStatus.Charge)
            {
                characterGO.GetComponent<CraneAnimator>().Charge();
                PlayerStatusManager.Instance().SetMoveStatus(EnumSpace.PlayStatus.Charge);
            }
        }

        if (Input.GetMouseButtonUp(0) && beginCD == false)
        {
            beginCD = true;
            cd = 1;
            characterGO.GetComponent<CraneAnimator>().Launch();
            PlayerStatusManager.Instance().SetMoveStatus(EnumSpace.PlayStatus.Launch);
            timer = 0;
            if(bulletTimeStatus == EnumSpace.BulletTimeStatus.IN)
            {
                StopBulletTime();
            }
        }
    }

    protected void CheckBulletTime()
    {
        if (Input.GetMouseButtonDown(1))
        {
            BulletTimeController.Instance.StartBulletTime(BULLET_TIME_DURATION);
            bulletTimeStatus = EnumSpace.BulletTimeStatus.IN;
            StartCoroutine(StartBulletTime());
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopBulletTime();
        }
    }

    protected override void PlayWalkAnim()
    {
        characterGO.GetComponent<CraneAnimator>().Walk();
    }

    protected override void PlayIdleAnim()
    {
        characterGO.GetComponent<CraneAnimator>().Idle();
    }

    void StopBulletTime()
    {
        Debug.Log(123455);
        BulletTimeController.Instance.StopBulletTime();
        bulletTimeStatus = EnumSpace.BulletTimeStatus.OUT;
        StopCoroutine(StartBulletTime());
    }

    IEnumerator StartBulletTime()
    {
        float t = 0f;
        while(t < 1f)
        {
            t += Time.unscaledDeltaTime / BULLET_TIME_DURATION;
            yield return null;
        }
        //结束将状态置为OUT
        bulletTimeStatus = EnumSpace.BulletTimeStatus.OUT;
    }
}
