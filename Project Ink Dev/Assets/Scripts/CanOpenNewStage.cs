using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanOpenNewStage : MonoBehaviour
{
    private static GameObject stageKey = null;

    private static bool CanOpenStage()
    {
        return GameMesMananger.GetMainItemNum(GameMesMananger.getCurStageNum())
            == GameMesMananger.GetCurMainItemNum(GameMesMananger.getCurStageNum());
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
        if (GameMesMananger.getCurStageNum() == 0)
        {
            stageKey = GameObject.Find("frog");
            stageKey.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
