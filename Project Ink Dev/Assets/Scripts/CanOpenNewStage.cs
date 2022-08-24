using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanOpenNewStage : MonoBehaviour
{
    private static GameObject stageKey = null;

    private static bool CanOpenStage()
    {
        return GameMesMananger.Instance().GetMainItemNum(GameMesMananger.Instance().getCurStageNum())
            == GameMesMananger.Instance().GetCurMainItemNum(GameMesMananger.Instance().getCurStageNum());
    }

    private static void OpenNewStage()
    {
        if (CanOpenStage())
            stageKey.SetActive(true);
        
    }

    public static void UpdateStage()
    {
        OpenNewStage();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
		if (GameMesMananger.Instance().getCurStageNum() == 0)
		{
			stageKey = GameObject.Find("frog");
			stageKey.SetActive(false);
		}

		if (GameMesMananger.Instance().getCurStageNum() == 1)
		{
			stageKey = GameObject.Find("crane");
			stageKey.SetActive(false);
		}

		//if (GameMesMananger.getCurStageNum() == 2)
		//{
		//    stageKey = GameObject.Find("frog");
		//    stageKey.SetActive(false);
		//}
	}

}
