using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCrane : BasePlayer
{
    private float timer = 0;
    EnumSpace.BulletTimeStatus bulletTimeStatus = EnumSpace.BulletTimeStatus.OUT;
    [Header("蓄力阈值")]
    public float threshold = 0.2f;
    [Header("子弹时间持续时间")]
    public float BULLET_TIME_DURATION = 3f;

    public float MAX_DASH_STORAGE_TIME = 1.0f;
    public float MAX_DASH_MULTI = 3.0f;
    public Image storageFull;

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
        if (isMouseButtonUp)
            CheckIsInCD();
        CheckBulletTime();
    }

    protected new void LetMove()
    {
        var camera = Camera.main;
        var screenRay = (camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
        Ray ray = new Ray(screenRay.origin + screenRay.direction * 4.3f, screenRay.direction);
        RaycastHit hitInfo;
        var multi = timer * MAX_DASH_MULTI;
        if (Physics.Raycast(ray, out hitInfo, DASH_DIS * multi))
        {
            //print(hitInfo.transform);
            moveDest = hitInfo.point - ray.direction * 1.0f;
            transform.DOMove(moveDest, LAST_TIME).SetEase(curv);

        }
        else
        {
            moveDest = ray.origin + ray.direction * DASH_DIS * multi;
            transform.DOMove(moveDest, LAST_TIME).SetEase(curv);
        }
    }

    protected override void CheckDash()
    {
        //按住鼠标timer开始计时
        if (Input.GetMouseButton(0) && beginCD == false)
        {
            timer += Time.deltaTime / MAX_DASH_STORAGE_TIME;
            if (timer > threshold && PlayerStatusManager.Instance().GetPlayerMoveStatus() != EnumSpace.PlayStatus.Charge)
            {
                characterGO.GetComponent<CraneAnimator>().Charge();
                PlayerStatusManager.Instance().SetMoveStatus(EnumSpace.PlayStatus.Charge);
            }
            if (timer > 1.0f)
                timer = 1.0f;
            storageFull.fillAmount = timer;
        }

        if (Input.GetMouseButtonUp(0) && beginCD == false)
        {
            isMouseButtonUp = true;
            beginCD = true;
            cd = 1;
            LetMove();

            characterGO.GetComponent<CraneAnimator>().Launch();
            PlayerStatusManager.Instance().SetMoveStatus(EnumSpace.PlayStatus.Launch);
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(LAST_TIME);
            seq.AppendCallback(PlayerStatusManager.Instance().SetStatusToIdle);
            timer = 0;
            storageFull.fillAmount = timer;
            if (bulletTimeStatus == EnumSpace.BulletTimeStatus.IN)
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

	public override Vector3 GetMoveDest()
	{
        return moveDest;
	}
}
