using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrog : BasePlayer
{
    private float timer = 0;

    [Header("������ֵ")]
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
        //����cd
        IsInCD();
    }

    protected override void MouseClick()
    {
        //��ס���timer��ʼ��ʱ
        if(Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            //��ס���ʱ�䳬����ֵ�ж�Ϊ������������������
            if (timer > threshold && moveStatus == EnumSpace.PlayStatus.Idle)
            {
                characterGO.GetComponent<FrogAnimator>().Charge();
                moveStatus = EnumSpace.PlayStatus.Charge;
            }
        }

        //̧����귢��
        if(Input.GetMouseButtonUp(0))
        {
            characterGO.GetComponent<FrogAnimator>().Launch();
            moveStatus = EnumSpace.PlayStatus.Idle;
            timer = 0;
        }
    }
}
