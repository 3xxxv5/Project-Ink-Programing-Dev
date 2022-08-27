using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMesManager : Singleton<GuideMesManager>
{
    public EnumSpace.GuideStatus guideStatus;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        if(GameMesMananger.Instance().save.isLevelPass[1] == false)
        {
            guideStatus = EnumSpace.GuideStatus.InGuide;
        }
    }
}
