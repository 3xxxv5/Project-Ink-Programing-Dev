using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class spawnItemTrigger : MonoBehaviour{

    Queue<GameObject> queue = new Queue<GameObject>();
    void spawnItem()
    {
        if(queue.Count>0)
		{
            var item = queue.Dequeue();
            item.SetActive(true);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Player"))
        {
            
            Sequence seq = DOTween.Sequence();
            for(int i=0;i<queue.Count;++i)
            {
                var randNumber = Random.Range(0.1f,1.0f);
                seq.AppendInterval(1.0f+randNumber);
                seq.AppendCallback(spawnItem);
            }
        }
    }

    void OnEnable()
    {
        for(int i=0;i<transform.childCount;++i)
        {
            var go = transform.GetChild(i).gameObject;
            queue.Enqueue(go);
            go.SetActive(false);
        }

        GetComponent<BoxCollider>().isTrigger = true;
    }


}