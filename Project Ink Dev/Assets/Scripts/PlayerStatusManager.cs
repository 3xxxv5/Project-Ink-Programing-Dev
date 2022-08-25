using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

public class PlayerStatusManager
{
	private static PlayerStatusManager instance = null;
	private static readonly object obj = new object();
	private PlayerStatusManager() { }
	public static PlayerStatusManager Instance()
	{
		if (instance == null)
		{
			lock (obj)
			{
				if (instance == null)
				{
					instance = new PlayerStatusManager();
				}
			}
		}

		return instance;
	}

	private EnumSpace.PlayStatus moveStatus;

	public EnumSpace.PlayStatus GetPlayerMoveStatus()
	{
		return moveStatus;
	}

	public void SetMoveStatus(EnumSpace.PlayStatus status)
	{
		moveStatus = status;
		//Debug.Log(status);
	}

	public void SetStatusToIdle()
	{
		SetMoveStatus(EnumSpace.PlayStatus.Idle);
	}

	public void SetToFaint()
	{
		SetMoveStatus(EnumSpace.PlayStatus.Faint);
	}


}
