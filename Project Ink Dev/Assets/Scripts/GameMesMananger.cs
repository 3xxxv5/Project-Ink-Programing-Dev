using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMesMananger : MonoBehaviour
{
    public int firstLevelneedMainItemNum = 15;
    public int firstLevelCurGetMainItemNum = 0;

    public int firstLevelneedHiddenItemNum = 5;
    public int firstLevelCurGetHiddenItemNum = 0;

    void Start()
    {
        //gameObject.GetComponent<UserInterface>().RefreshScore(firstLevelCurGetMainItemNum, firstLevelneedMainItemNum);
    }

    public void updateUI()
	{
        gameObject.GetComponent<UserInterface>().RefreshScore(firstLevelCurGetMainItemNum, firstLevelneedMainItemNum);
	}

    // Update is called once per frame
    void Update()
    {
        if(firstLevelCurGetMainItemNum == firstLevelneedMainItemNum)
		{
            //doSth
		}

        if(firstLevelneedHiddenItemNum == firstLevelCurGetHiddenItemNum)
		{
            //doSth
		}
    }
}
