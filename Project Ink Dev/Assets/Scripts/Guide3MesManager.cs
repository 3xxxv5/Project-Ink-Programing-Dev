using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide3MesManager : Singleton<Guide3MesManager>
{
    public EnumSpace.GuideStatus guideStatus;

    protected override void Awake()
    {
        base.Awake();
        if (GameMesMananger.Instance().save.isLevelPass[3] == false)
        {
            guideStatus = EnumSpace.GuideStatus.InGuide;
        }
    }
}
