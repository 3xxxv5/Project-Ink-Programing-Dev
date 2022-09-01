using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameMesMananger
{
	private static GameMesMananger instance = null;
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
	}

	int[] mainItemNum = new int[3] { 15, 20, 28 };
	private  int[] curGetMainItemNum = new int[3] { 0 ,0, 0};

	private  int[] hiddenItemNum = new int[3] { 5, 5, 7};
	private  int[] curGetHiddenItemNum = new int[3] { 0, 0, 0};

	private  int curStageNum = -1;
	public Save save = SaveManager.LoadByJSON();

	public Dictionary<GameObject, string> map1 = new Dictionary<GameObject, string>();
	public Dictionary<string, int> map2 = new Dictionary<string, int>();

	private EnumSpace.GameMode GAME_MODE = EnumSpace.GameMode.Start;

	public EnumSpace.GameMode getGameMode()
	{
		return GAME_MODE;
	}

	public void SetGameModeStart()
	{
		GAME_MODE = EnumSpace.GameMode.Start;
	}

	public void SetGameModeEnd()
	{
		GAME_MODE = EnumSpace.GameMode.End;
	}

	public  int getCurStageNum()
	{
		return curStageNum;
	}

	
	public  void SetStage(int stageNum)
	{
		curStageNum = stageNum;
		//Debug.Log(curStageNum);
		//////////////////////////////////
		DOTween.Clear(true);
	}

	public  void SetCurMainItemNumAdd(int curStageNum)
	{
		curGetMainItemNum[curStageNum]++;
	}

	public  void SetCurHiddenItemNumAdd(int curStageNum)
	{
		curGetHiddenItemNum[curStageNum]++;
	}

	public  void SetCurMainItemNum(int curStageNum,int num)
	{
		curGetMainItemNum[curStageNum] = num;
	}

	public  void SetCurHiddenItemNum(int curStageNum,int num)
	{
		curGetHiddenItemNum[curStageNum] = num;
	}

	public  int GetCurMainItemNum(int curStageNum)
	{
		return curGetMainItemNum[curStageNum];
	}

	public  int GetCurHiddenItemNum(int curStageNum)
	{
		return curGetHiddenItemNum[curStageNum];
	}

	public  int GetMainItemNum(int curStageNum)
	{
		return mainItemNum[curStageNum];
	}

	public  int GetHiddenItemNum(int curStageNum)
	{
		return hiddenItemNum[curStageNum];
	}

	public void Clear()
	{
		curGetMainItemNum[0] = 0;
		curGetMainItemNum[1] = 0;
		curGetMainItemNum[2] = 0;
		curGetHiddenItemNum[0] = 0;
		curGetHiddenItemNum[1] = 0;
		curGetHiddenItemNum[2] = 0;
		map1.Clear();
		map2.Clear();
		SetGameModeStart();
	}

	void OnEnable()
    {	
		////////////////////////////////////////////////////////////////////

		///////////////////////////////////////////////////////////////////

		//////////////////////////////////////////////////////////////////

		//ui = gameObject.AddComponent<UserInterface>();
        //ui.RefreshScore(curGetMainItemNum[curStageNum], mainItemNum[curStageNum]);
    }

 //   public static void updateUI()
	//{
	//	ui.RefreshScore(curGetMainItemNum[curStageNum], mainItemNum[curStageNum]);
	//	ui.RefreshHideScore(curGetHiddenItemNum[curStageNum], hiddenItemNum[curStageNum]);
	//}



    // Update is called once per frame
    void Update()
    {

    }

	private void OnDisable()
	{
		
	}

	private void OnDestroy()
	{
		//ui = null;
	}
}
