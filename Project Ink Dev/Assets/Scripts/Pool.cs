using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Pool
{
    public GameObject Prefab => prefab;

    [SerializeField] GameObject prefab;

    Queue<GameObject> queue;

    [SerializeField]  int size = 5;

    Transform parent;

    //初始化对象池
    public void Initialize(Transform parent)
	{
        queue = new Queue<GameObject>();
        this.parent = parent;

        for(int i=0;i<size;++i)
		{
            queue.Enqueue(Copy());
		}
	}

    GameObject Copy()
	{
        var copy = GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);
        return copy;
	}

    GameObject GetObjectOutOfPool()
	{
        GameObject go = null;

        if(queue.Count > 0 && !queue.Peek().activeSelf)
		{
            go = queue.Dequeue();
		}
        else
		{
            go = Copy();
		}

        queue.Enqueue(go);
        return go;
	}

    public GameObject GetObject()
	{
        var go = GetObjectOutOfPool();
        go.SetActive(true);
        return go;
	}

    public GameObject GetObject(Vector3 position,Quaternion rotation)
    {
        var go = GetObjectOutOfPool();
        go.SetActive(true);
        go.transform.position = position;
        go.transform.rotation = rotation;
        return go;
    }
}
