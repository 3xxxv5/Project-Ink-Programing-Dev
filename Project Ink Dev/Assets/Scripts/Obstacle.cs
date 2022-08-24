using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;
using DG.Tweening;

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
            var playerGo = other.gameObject;
            //var player = playerGo.GetComponent<Player>();
            var camera = Camera.main;

            PlayerStatusManager.Instance().SetToFaint();
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(2.0f);
            seq.AppendCallback(PlayerStatusManager.Instance().SetStatusToIdle);
            var screenRay = (camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
            Vector3 aim = playerGo.transform.position - (screenRay.direction  * 5.0f);
            playerGo.transform.DOMove(aim,2.0f);
            camera.DOShakePosition(0.5f);
            camera.DOShakeRotation(0.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
