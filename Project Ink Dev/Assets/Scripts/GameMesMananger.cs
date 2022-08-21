using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMesMananger : MonoBehaviour
{
	/*private static GameMesMananger instance = null;
	private static readonly object obj = new object();
	private GameMesMananger() { }
	public static GameMesMananger Instance()
	{
		if (instance == null)
		{
			lock (obj)
			{
				if (instance == null)
				{
					instance = new GameMesMananger();
				}
			}
		}

		return instance;
	}*/

	public static int firstLevelneedMainItemNum = 15;
	public static int firstLevelCurGetMainItemNum = 0;

	 public static int firstLevelneedHiddenItemNum = 5;
	 public static int firstLevelCurGetHiddenItemNum = 0;



	private static UserInterface ui = null;

    void Start()
    {
		
		ui = gameObject.AddComponent<UserInterface>();
        ui.RefreshScore(firstLevelCurGetMainItemNum, firstLevelneedMainItemNum);
    }

    public static void updateUI()
	{
		ui.RefreshScore(firstLevelCurGetMainItemNum, firstLevelneedMainItemNum);
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

	private void OnDestroy()
	{
		ui = null;
	}
}
