using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMesMananger : MonoBehaviour
{
    public int firstLevelneedMainItemNum = 15;
    public int firstLevelCurGetMainItemNum = 0;

    public int firstLevelneedHiddenItemNum = 5;
    public int firstLevelCurGetHiddenItemNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
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
