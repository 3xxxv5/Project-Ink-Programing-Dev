using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    private static UserInterface ui = null;

    void Start()
    {
        ui = gameObject.GetComponent<UserInterface>();
    }

    public static void updateUI()
    {
        var stageNum = GameMesMananger.Instance().getCurStageNum();

        ui.RefreshScore(GameMesMananger.Instance().GetCurMainItemNum(stageNum)
            , GameMesMananger.Instance().GetMainItemNum(stageNum));
        
        ui.RefreshHideScore(GameMesMananger.Instance().GetCurHiddenItemNum(GameMesMananger.Instance().getCurStageNum())
            , GameMesMananger.Instance().GetHiddenItemNum(GameMesMananger.Instance().getCurStageNum()));

        //Debug.Log("updateUI");
    }

 //   public static void SetStageControllerEnabled()
	//{
 //       stageController.enabled = true;
	//}

 //   public static void SetStageControllerDisabled()
	//{
 //       stageController.enabled = false;
	//}
}
