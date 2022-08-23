using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;


public class PlayerShrimp : BasePlayer
{
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

        //更新cd
        if (isMouseButtonUp)
            CheckIsInCD();
    }

    protected override void CheckDash()
    {
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
            cd = 1;
            isMouseButtonUp = true;

            //播放冲刺动画
            characterGO.GetComponent<ShrimpAnimator>().Dash();
            SetMoveStatus(EnumSpace.PlayStatus.Dash);
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(LAST_TIME);
            seq.AppendCallback(SetStatusToIdle);
        }
    }

    protected override void PlayWalkAnim()
    {
        characterGO.GetComponent<ShrimpAnimator>().Walk();
    }

    protected override void PlayIdleAnim()
    {
        characterGO.GetComponent<ShrimpAnimator>().Idle();
    }
}
