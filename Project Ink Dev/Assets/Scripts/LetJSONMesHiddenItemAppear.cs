using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetJSONMesHiddenItemAppear : MonoBehaviour
{
    void Start()
    {
        GameUIManager.updateUI();
        var hasGetHiddenNum = GameMesMananger.Instance().save.hideCollections[GameMesMananger.Instance().getCurStageNum()];
        if(hasGetHiddenNum > 0)
		{
            for(var i=0;i<hasGetHiddenNum;++i)
			{
                GameMesMananger.Instance().SetCurHiddenItemNumAdd(GameMesMananger.Instance().getCurStageNum());
                GameUIManager.updateUI();
			}
		}
    }
}
