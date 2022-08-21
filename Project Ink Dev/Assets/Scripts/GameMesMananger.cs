using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

	public static int[] mainItemNum = new int[3];
	public static int[] curGetMainItemNum = new int[3];

	 public static int[] hiddenItemNum = new int[3];
	 public static int[] curGetHiddenItemNum = new int[3];

	private static UserInterface ui = null;

	private static int curStageNum = 0;

	public static int getCurStageNum()
	{
		return curStageNum;
	}

	public static void SetStage(int stageNum)
	{
		curStageNum = stageNum;
		Debug.Log(curStageNum);
		updateUI();
		//////////////////////////////////
		DOTween.Clear(true);
		//////////////////////////////////
		///stage1



		//////////////////////////////////
	}

	public static GameObject getGameMesGameObject()
	{
		return GameObject.Find("Game_Msg_Manager");
	}

	public static void SetCurMainItemNumAdd(int curStageNum)
	{
		curGetMainItemNum[curStageNum]++;
	}

	public static void SetCurHiddenItemNumAdd(int curStageNum)
	{
		curGetHiddenItemNum[curStageNum]++;
	}

	public static void SetCurMainItemNum(int curStageNum,int num)
	{
		curGetMainItemNum[curStageNum] = num;
	}

	public static void SetCurHiddenItemNum(int curStageNum,int num)
	{
		curGetHiddenItemNum[curStageNum] = num;
	}

	public static int GetCurMainItemNum(int curStageNum)
	{
		return curGetMainItemNum[curStageNum];
	}

	public static int GetCurHiddenItemNum(int curStageNum)
	{
		return curGetHiddenItemNum[curStageNum];
	}

	public static int GetMainItemNum(int curStageNum)
	{
		return mainItemNum[curStageNum];
	}

	public static int GetHiddenItemNum(int curStageNum)
	{
		return hiddenItemNum[curStageNum];
	}

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void OnEnable()
    {	

		mainItemNum[0] = 15;
		curGetMainItemNum[0] = 0;

		hiddenItemNum[0] = 5;
		curGetHiddenItemNum[0] = 0;

		mainItemNum[1] = 20;
		curGetMainItemNum[1] = 0;

		hiddenItemNum[1] = 5;
		curGetHiddenItemNum[1] = 0;

		mainItemNum[2] = 28;
		curGetMainItemNum[2] = 0;

		hiddenItemNum[2] = 7;
		curGetHiddenItemNum[2] = 0;

		////////////////////////////////////////////////////////////////////

		///////////////////////////////////////////////////////////////////

		//////////////////////////////////////////////////////////////////

		ui = gameObject.AddComponent<UserInterface>();
        //ui.RefreshScore(curGetMainItemNum[curStageNum], mainItemNum[curStageNum]);
    }

    public static void updateUI()
	{
		ui.RefreshScore(curGetMainItemNum[curStageNum], mainItemNum[curStageNum]);
		ui.RefreshHideScore(curGetHiddenItemNum[curStageNum], hiddenItemNum[curStageNum]);
	}



    // Update is called once per frame
    void Update()
    {

    }

	private void OnDisable()
	{
		
	}

	private void OnDestroy()
	{
		ui = null;
	}
}
