using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Item : MonoBehaviour
{
    public ItemType itemType;
    public InteractiveType interactiveType;

    public float m_velocity = 1.0f;
    public float m_acceleratetion = 0f;
    public float m_rebirthTime = 3.0f;
    public Vector3 m_originalPosition; //记录掉落物的生成位置,在OnTriggerEnter实现循环掉落逻辑;将记录界限的Trigger的Tag设为RepeatOrDisappear;

    public GameObject VFXPrefab;

    public GameObject splitItem;
    public float SPLIT_FLY_TIME = 1.0f;

    public abstract void SetFallingTrack(); //掉落轨迹,每一个实现该方法的子类都应该在update()中调用该方法

    public abstract void DropRebirth();

    //其它对象进入触发器时的逻辑
    //if(other.gameObject.tag.Contains("Player"))	{doSth;}
    //if(other.gameObject.tag.Contains("RepeatOrDisappear"))	{doSth;}
    //上述语句必须在实现接口中实现；
    protected abstract void OnTriggerEnter(Collider other);


    protected void init()
    {
        m_originalPosition = transform.position;
        GetComponent<BoxCollider>().isTrigger = true;
        gameObject.tag = "Item";

    }

    protected void SetOriginalPosition(Vector3 pos)
	{
        m_originalPosition = pos;
	}

    protected void SpawnSplitItem(float x,float z)
	{
        var playerGO = GameObject.FindGameObjectWithTag("Player");
        int LEFT_NUM = 4;
        GameMesMananger.Instance().map2.Add(gameObject.name, LEFT_NUM);
        for(int i=-1;i<1;++i)
		{
            var newSplitItem1 = GameObject.Instantiate(splitItem, transform.position,
                transform.rotation);
            newSplitItem1.tag = "NeedCollectFourDrop";
            newSplitItem1.GetComponent<BoxCollider>().enabled = false;

            var newSplitItem2 = GameObject.Instantiate(splitItem, transform.position,
                transform.rotation);
            newSplitItem2.tag = "NeedCollectFourDrop";
            newSplitItem2.GetComponent<BoxCollider>().enabled = false;

            PlayerShrimp shrimp = null;
            PlayerFrog frog = null;
            PlayerCrane crane = null;
            if (playerGO.GetComponent<PlayerShrimp>())
                shrimp = playerGO.GetComponent<PlayerShrimp>();
            if (playerGO.GetComponent<PlayerFrog>())
                frog = playerGO.GetComponent<PlayerFrog>();
            if (playerGO.GetComponent<PlayerCrane>())
                crane = playerGO.GetComponent<PlayerCrane>();

            Vector3 moveDest1 = Vector3.zero;
            Vector3 moveDest2 = Vector3.zero;
            i = i < 0 ? -1 : 1;
            if(shrimp)
			{
                //moveDest1 = shrimp.GetMoveDest() + new Vector3(i * x, 0, -i * z);
                //moveDest2 = shrimp.GetMoveDest() + new Vector3(i * x, 0, i * z);

                moveDest1 = crane.GetMoveDest() + playerGO.transform.right * x * i + playerGO.transform.forward * x;
                moveDest2 = crane.GetMoveDest() + playerGO.transform.right * z * i + playerGO.transform.forward * z;
            }
            else if(frog)
			{
                //moveDest1 = frog.GetMoveDest() + new Vector3(i * x, 0, -i * z);
                //moveDest2 = frog.GetMoveDest() + new Vector3(i * x, 0, i * z);

                moveDest1 = crane.GetMoveDest() + playerGO.transform.right * x * i + playerGO.transform.forward * x;
                moveDest2 = crane.GetMoveDest() + playerGO.transform.right * z * i + playerGO.transform.forward * z;
            }
            else if(crane)
			{
                //moveDest1 = crane.GetMoveDest() + new Vector3(i * x, 0, -i * z);
                //moveDest2 = crane.GetMoveDest() + new Vector3(i * x, 0, i * z);

                moveDest1 = crane.GetMoveDest() + playerGO.transform.right * x * i + playerGO.transform.forward * x;
                moveDest2 = crane.GetMoveDest() + playerGO.transform.right * z * i + playerGO.transform.forward * z;
            }

            newSplitItem1.transform.DOMove(moveDest1, SPLIT_FLY_TIME);
            newSplitItem2.transform.DOMove(moveDest2, SPLIT_FLY_TIME);

            newSplitItem1.GetComponent<FlowerSplit>().SetOriginalPosition(moveDest1);
            newSplitItem2.GetComponent<FlowerSplit>().SetOriginalPosition(moveDest2);

            /////////////////////////////////////////////////////////////////////////////////
            GameMesMananger.Instance().map1.Add(newSplitItem1, gameObject.name);
            GameMesMananger.Instance().map1.Add(newSplitItem2, gameObject.name);
        }
	}

    

    /*protected void DefaultOnTriggerEnterImplement(Collider other)
    {
        if(other.gameObject.tag.Contains("Player"))
        {
            var player = GameObject.Find("Player_1");
            if (player.GetComponent<Player>().GetPlayerMoveStatus() == PlayStatus.Dash)
            {
                if (itemType == ItemType.Main)
                {
                    if (interactiveType == InteractiveType.Type1)
                    {
                        //main_item_collection+1
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
                        Destroy(this.gameObject);
                    }
                    if (interactiveType == InteractiveType.Type2)
                    {
                        //doSth
                    }
                }
            }
            
        }

        if(other.gameObject.tag.Contains("RepeatOrDisappear"))
        {
            if(itemType == ItemType.Main)
            {
                transform.position = m_originalPosition;
            }
            else if(itemType == ItemType.Hidden)
            {
                Destroy(this.gameObject);
            }
        }
    }*/

}