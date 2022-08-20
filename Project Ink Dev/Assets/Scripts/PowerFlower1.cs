using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;
using DG.Tweening;

public class PowerFlower1 : Item
{
    
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

    void CollideWithPlayerBehavior(Collider other)
	{
        //GameObject gameMsgGo = GameObject.Find("Game_Msg_Manager");
        //var manager = gameMsgGo.GetComponent<GameMesMananger>();
        if (itemType == ItemType.Main)
        {
            
            if (interactiveType == InteractiveType.Type1)
            {
                //main_item_collection+1
                GameMesMananger.firstLevelneedMainItemNum++;

               // Camera.main.DOShakeRotation(0.5f);

                Destroy(this.gameObject);

            }
            if (interactiveType == InteractiveType.Type2)
            {
                //doSth
            }
        }

        if (itemType == ItemType.Hidden)
        {
            if (interactiveType == InteractiveType.Type1)
            {
                //hidden_item_collection+1
                GameMesMananger.firstLevelCurGetHiddenItemNum++;

                //Camera.main.DOShakeRotation(0.5f);

                Destroy(this.gameObject);
            }
            if (interactiveType == InteractiveType.Type2)
            {
                //doSth
            }
        }

        DropMusicPlay.PlayMusic();
        Boom(other);
        if (CameraStatusController.Instance().GetCameraStatus()==CameraStatus.Common)
        {
            SetCameraShock();
        }
        GameMesMananger.updateUI();
    }


    void CollideWithRebirthBehavior()
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
    }

    protected override void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
		{
            case "Player":
                if (other.gameObject.GetComponent<Player>().GetPlayerMoveStatus() == PlayStatus.Dash)
				{
                    CollideWithPlayerBehavior(other);
				}
                break;
            case "RepeatOrDisappear":
                CollideWithRebirthBehavior();
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

    void OnEnable()
    {
        init();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0f,-m_velocity,0f);
    }

    void FixedUpdate()
    {
        SetFallingTrack();
    }
}