using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide2MesManager : Singleton<Guide2MesManager>
{
    public EnumSpace.GuideStatus guideStatus;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        if (GameMesMananger.Instance().save.isLevelPass[1] == false)
        {
            guideStatus = EnumSpace.GuideStatus.InGuide;
        }
    }
}
