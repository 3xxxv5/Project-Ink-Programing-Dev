using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public bool canDestroy = false;
    public bool canSetAct = true;
    public float lifeTime = 3.0f;

    WaitForSeconds waitLifeTime;

    
    void Awake()
    {
        waitLifeTime = new WaitForSeconds(lifeTime);
    }

	void OnEnable()
	{
        StartCoroutine(LifeCoroutine());
	}

	IEnumerator LifeCoroutine()
    {
        yield return waitLifeTime;

        if(canDestroy)
		{
            Destroy(gameObject);
		}
        else if(canSetAct)
		{
            gameObject.SetActive(false);
		}
    }
}
