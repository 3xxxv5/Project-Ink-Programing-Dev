using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerFrog : BasePlayer
{
    private float timer = 0;
    [Header("蓄力阈值")]
    public float threshold = 0.2f;
    public float MAX_DASH_STORAGE_TIME = 1.0f;
    public float MAX_DASH_MULTI = 3.0f;
    public Image storageFull;

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
        if (isMouseButtonUp)
            CheckIsInCD();
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
            transform.DOMove(hitInfo.point - ray.direction * 1.0f, LAST_TIME).SetEase(curv);
        }
        else
        {
            transform.DOMove(ray.origin + ray.direction * DASH_DIS * multi, LAST_TIME).SetEase(curv);
        }
    }

    protected override void CheckDash()
    {
        //按住鼠标timer开始计时
        if(Input.GetMouseButton(0) && beginCD == false)
        {
            timer += Time.deltaTime / MAX_DASH_STORAGE_TIME;

            //按住鼠标时间超过阈值判定为蓄力，播放蓄力动画
            if (timer > threshold && moveStatus != EnumSpace.PlayStatus.Charge)
            {
                //toDo
                characterGO.GetComponent<FrogAnimator>().Charge();
                SetMoveStatus(EnumSpace.PlayStatus.Charge);
            }
            if (timer > 1.0f)
                timer = 1.0f;
            storageFull.fillAmount = timer;
        }

        //抬起鼠标发射
        if(Input.GetMouseButtonUp(0) && beginCD == false)
        {
            //判断条件不同，抬起鼠标才开始计算cd
            isMouseButtonUp = true;
            beginCD = true;
            cd = 1;

            LetMove();

            characterGO.GetComponent<FrogAnimator>().Launch();
            SetMoveStatus(EnumSpace.PlayStatus.Launch);
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(LAST_TIME);
            seq.AppendCallback(SetStatusToIdle);
            //seq.AppendCallback(SetCanStorage);
            timer = 0;
            storageFull.fillAmount = timer;
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
