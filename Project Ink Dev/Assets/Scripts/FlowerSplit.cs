using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;
using DG.Tweening;

public class FlowerSplit : Item
{
	private float rebirthTimeTemp;
	private Rigidbody rb;

	void Boom(Collider other)
	{
		PoolManager.release(VFXPrefab, other.transform.position, other.transform.rotation);
	}

	void SetCameraShock()
	{
		CameraStatusController.Instance().SetShock();
		Camera.main.DOShakePosition(0.5f);

		Sequence seq = DOTween.Sequence();
		seq.AppendInterval(0.5f);
		seq.AppendCallback(CameraStatusController.Instance().SetCommon);
	}

	public override void DropRebirth()
	{
		if (rebirthTimeTemp < 0.00001f)
		{
			rebirthTimeTemp = m_rebirthTime;

			switch (itemType)
			{
				case ItemType.Main:
					//Debug.Log("Rebirth");
					transform.position = m_originalPosition;
					break;
				case ItemType.Hidden:
					//Debug.Log("Destroy");
					Destroy(this.gameObject);
					break;
			}
		}

		rebirthTimeTemp -= Time.deltaTime;
	}

	public override void SetFallingTrack()
	{
		var m = rb.mass;
		Vector3 force = new Vector3(0f, -m * m_acceleratetion, 0f);
		rb.AddForce(force);
	}

	protected override void OnTriggerEnter(Collider other)
	{
		switch (other.gameObject.tag)
		{
			case "Player":
				if (PlayerStatusManager.Instance().GetPlayerMoveStatus() == PlayStatus.Dash)
				{
					CollideWithPlayerBehavior(other);
				}
				break;
		}
	}

	void CollideWithPlayerBehavior(Collider other)
	{
		var stageNum = GameMesMananger.Instance().getCurStageNum();
		if(gameObject.tag.Contains("NeedCollectFourDrop"))
		{
			--GameMesMananger.Instance().map[gameObject];
			Debug.Log(GameMesMananger.Instance().map[gameObject]);
			if(GameMesMananger.Instance().map[gameObject]==0)
			{
				switch(itemType)
				{
					case ItemType.Main:
						GameMesMananger.Instance().SetCurMainItemNumAdd(stageNum);
						break;
					case ItemType.Hidden:
						GameMesMananger.Instance().SetCurHiddenItemNumAdd(stageNum);
						break;
				}
			}
		}

		DropMusicPlay.PlayMusic();
		Boom(other);
		if (CameraStatusController.Instance().GetCameraStatus() == CameraStatus.Common)
		{
			SetCameraShock();
		}
		GameUIManager.updateUI();

		CanOpenNewStage.UpdateStage();
		Destroy(this.gameObject);
	}

	void SetTrigger()
	{
		gameObject.GetComponent<BoxCollider>().isTrigger = true;
	}

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.velocity = new Vector3(0f, -m_velocity, 0f);
		rebirthTimeTemp = m_rebirthTime;
		gameObject.GetComponent<BoxCollider>().isTrigger = false;
		Sequence seq = DOTween.Sequence();
		seq.AppendInterval(SPLIT_FLY_TIME);
		seq.AppendCallback(SetTrigger);
	}

	void Update()
	{
		DropRebirth();
	}

	void FixedUpdate()
	{
		SetFallingTrack();
	}
}
