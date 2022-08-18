using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] particlesPool;

	static Dictionary<GameObject, Pool> map;

	void Start()
	{
		map = new Dictionary<GameObject, Pool>();
		Initialize(particlesPool);
	}

	void Initialize(Pool[] pools)
	{ 
		foreach(var pool in pools)
		{
			map.Add(pool.Prefab, pool);

			Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;
			poolParent.parent = transform;

			pool.Initialize(poolParent);
		}
	}

	public static GameObject release(GameObject prefab)
	{
		return map[prefab].GetObject();
	}

	public static GameObject release(GameObject prefab,Vector3 position,Quaternion rotation)
	{
		return map[prefab].GetObject(position, rotation);
	}
}
