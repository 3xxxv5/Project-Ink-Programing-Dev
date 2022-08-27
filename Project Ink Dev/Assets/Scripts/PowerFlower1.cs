using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;
using DG.Tweening;

public class PowerFlower1 : Item
{
    
    private Rigidbody rb;
    private float rebirthTimeTemp;


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

    void CollideWithPlayerBehavior(Collider other)
	{
        var stageNum = GameMesMananger.Instance().getCurStageNum();
        Debug.Log(stageNum);
        if (itemType == ItemType.Main)
        {
            
            if (interactiveType == InteractiveType.Type1)
            {
                //main_item_collection+1
                GameMesMananger.Instance().SetCurMainItemNumAdd(stageNum);
                
                // Camera.main.DOShakeRotation(0.5f);



            }
            if (interactiveType == InteractiveType.Type2)
            {
                //doSth
                if (splitItem == null)
                {
                    Debug.LogError("如果是类型2的掉落物，请挂上撞击后分裂的物体");
                    return;
                }

                SpawnSplitItem(10.0f, 10.0f);
            }
        }

        if (itemType == ItemType.Hidden)
        {
            if (interactiveType == InteractiveType.Type1)
            {
                //hidden_item_collection+1
                GameMesMananger.Instance().SetCurHiddenItemNumAdd(stageNum);
                if(GameMesMananger.Instance().save != null)
				{
                    if(GameMesMananger.Instance().save.itemMap.Find(x => x.Equals(gameObject.name))==null)
					{
                        GameMesMananger.Instance().save.itemMap.Add(gameObject.name);
					}
                }

                //if (GameMesMananger.Instance().save.itemMap[gameObject.name])
                //    Debug.Log(gameObject.name);

            }
            if (interactiveType == InteractiveType.Type2)
            {
                //doSth
                if (splitItem == null)
                {
                    Debug.LogError("如果是类型2的掉落物，请挂上撞击后分裂的物体");
                    return;
                }

                SpawnSplitItem(10.0f, 10.0f);
            }
        }

        DropMusicPlay.PlayMusic();
        Boom(other);
        if (CameraStatusController.Instance().GetCameraStatus()==CameraStatus.Common)
        {
            SetCameraShock();
        }
        Debug.Log("Main: "+GameMesMananger.Instance().GetCurMainItemNum(stageNum));
        Debug.Log("Hidden: " + GameMesMananger.Instance().GetCurHiddenItemNum(stageNum));
        GameUIManager.updateUI();

        CanOpenNewStage.UpdateStage();
        Debug.Log(PlayerStatusManager.Instance().GetPlayerMoveStatus());
        Destroy(this.gameObject);
    }


    /*void CollideWithRebirthBehavior()
	{
        if (itemType == ItemType.Main)
        {
            transform.position = m_originalPosition;
            Debug.Log("rebirth");
        }
        else if (itemType == ItemType.Hidden)
        {
            Debug.Log("Destroy");
            Destroy(this.gameObject);
        }
    }*/

    protected override void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
		{
            case "Player":
                if (PlayerStatusManager.Instance().GetPlayerMoveStatus() == PlayStatus.Dash || 
                    PlayerStatusManager.Instance().GetPlayerMoveStatus() == PlayStatus.Launch)
				{
                    CollideWithPlayerBehavior(other);
				}
                break;
            case "RepeatOrDisappear":
                //CollideWithRebirthBehavior();
                break;
        }
        //DefaultOnTriggerEnterImplement(other);
    }

    public override void SetFallingTrack()
    {
        var m = rb.mass;
        Vector3 force = new Vector3(0f,-m*m_acceleratetion,0f);
        rb.AddForce(force);
    }

    
	public override void DropRebirth()
	{
		if(m_rebirthTime < 0.00001f)
		{
            m_rebirthTime = rebirthTimeTemp;

            switch(itemType)
			{
                case  ItemType.Main:
                    //Debug.Log("Rebirth");
                    transform.position = m_originalPosition;
                    break;
                case ItemType.Hidden:
                    //Debug.Log("Destroy");
                    Destroy(this.gameObject);
                    break;
            }
		}

        m_rebirthTime -= Time.deltaTime;
	}

	void OnEnable()
    {
        init();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0f,-m_velocity,0f);
        rebirthTimeTemp = m_rebirthTime;
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