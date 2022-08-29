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
        if (PlayerStatusManager.Instance().GetPlayerMoveStatus() != EnumSpace.PlayStatus.Faint &&
            PlayerStatusManager.Instance().GetPlayerMoveStatus() != EnumSpace.PlayStatus.Dash)
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
        if (Input.GetMouseButtonDown(0))
        {
            if (beginCD == false)
            {
                LetMove();
                beginCD = true;
                cd = 1;
                isMouseButtonUp = true;

                //播放冲刺动画
                characterGO.GetComponent<ShrimpAnimator>().Dash();
                PlayerStatusManager.Instance().SetMoveStatus(EnumSpace.PlayStatus.Dash);
                Sequence seq = DOTween.Sequence();
                seq.AppendInterval(LAST_TIME);
                seq.AppendCallback(PlayerStatusManager.Instance().SetStatusToIdle);
            }
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

	public override Vector3 GetMoveDest()
	{
        return moveDest;
	}
}
