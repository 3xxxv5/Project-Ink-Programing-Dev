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

    void CollideWithPlayerBehavior(Collider other)
	{
        GameObject gameMsgGo = GameObject.Find("Game_Msg_Manager");
        var manager = gameMsgGo.GetComponent<GameMesMananger>();
        if (itemType == ItemType.Main)
        {
            
            var go = transform.parent.parent.Find("Play_Music");
            go.GetComponent<DropMusicPlay>().PlayMusic();
            if (interactiveType == InteractiveType.Type1)
            {
                //main_item_collection+1
                manager.firstLevelCurGetMainItemNum++;
                manager.updateUI();

                Boom(other);
                Camera.main.DOShakePosition(0.5f);
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
            var go = transform.parent.parent.Find("Play_Music");
            go.GetComponent<DropMusicPlay>().PlayMusic();
            if (interactiveType == InteractiveType.Type1)
            {
                //hidden_item_collection+1
                manager.firstLevelCurGetHiddenItemNum++;
                manager.updateUI();

                Boom(other);
                Camera.main.DOShakePosition(0.5f);
                //Camera.main.DOShakeRotation(0.5f);

                Destroy(this.gameObject);
            }
            if (interactiveType == InteractiveType.Type2)
            {
                //doSth
            }
        }
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
                var player = GameObject.Find("Player_1");
                if (player.GetComponent<Player>().moveStatus == PlayStatus.Dash)
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