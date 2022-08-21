using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrog : BasePlayer
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
        //º∆À„cd
        IsInCD();
    }

    protected override void MouseClick()
    {
        throw new System.NotImplementedException();
    }
}
