using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

[RequireComponent(typeof(BoxCollider))]
public class spawnItemTrigger : MonoBehaviour{

    Queue<GameObject> queue = new Queue<GameObject>();
    void spawnItem()
    {
        var item = queue.Dequeue();
        item.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if(transform.gameObject.tag.Contains("Player"))
        {

        }
    }

    void OnEnable()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        gameObject.tag = "SpawnItem";
        Debug.Log(string.Format("{0}{1}","p","q"));

        for(int i=0;i<transform.childCount;++i)
        {
            var go = transform.GetChild(i).gameObject;
            queue.Enqueue(go);
            go.SetActive(false);
        }

        //queue.Count
        for(int i=0;i<transform.childCount;++i)
        {
            int time = (i+1)*2;
            Invoke("xxxx",time);
        }
    }

    void Update()
    {

    }
}