using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag.Contains("Player"))
		{
            print("obstacle success");
		}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
