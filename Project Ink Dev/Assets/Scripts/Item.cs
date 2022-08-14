using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Item : MonoBehaviour
{
    public ItemType itemType;
    public InteractiveType interactiveType;

    public float m_velocity = 1.0f;
    public float m_acceleratetion = 0f;
    protected Vector3 m_originalPosition; //记录掉落物的生成位置,在OnTriggerEnter实现循环掉落逻辑;将记录界限的Trigger的Tag设为RepeatOrDisappear;

    public abstract void SetFallingTrack(); //掉落轨迹,每一个实现该方法的子类都应该在update()中调用该方法

    //其它对象进入触发器时的逻辑
    //if(other.gameObject.tag.Contains("Player"))	{doSth;}
    //if(other.gameObject.tag.Contains("RepeatOrDisappear"))	{doSth;}
    //上述语句必须在实现接口中实现；
    protected abstract void OnTriggerEnter(Collider other);


    //每一个实现该类的子类都应该在OnEnable()中调用该方法
    protected void init()
    {
        m_originalPosition = transform.position;
        GetComponent<BoxCollider>().isTrigger = true;
        gameObject.tag = "Item";
    }

    protected void DefaultOnTriggerEnterImplement(Collider other)
    {
        if(other.gameObject.tag.Contains("Player"))
        {
            var player = GameObject.Find("Player_1");
            if (player.GetComponent<Player>().moveStatus == PlayStatus.Dash)
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
    }

}