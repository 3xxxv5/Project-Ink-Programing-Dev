using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;


public class Player : BasePlayer
{
    void Start()
    {
        Init();
    }

    void Update()
    {
        if (moveStatus != EnumSpace.PlayStatus.Faint)
            PlayerMove();
        //计算cd
        IsInCD();
    }

    protected override void MouseClick()
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
            CDCount = 1;
            //
            Sequence seq = DOTween.Sequence();
            moveStatus = EnumSpace.PlayStatus.Dash;
            seq.AppendInterval(lastTime);
            seq.AppendCallback(SetStatusToIdle);
        }
    }
}
