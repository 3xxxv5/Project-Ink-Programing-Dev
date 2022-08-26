using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CraneItem : MonoBehaviour
{
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //PlayerPrefs.SetInt("PoMoLevel" + 1, 1);
            DOTween.Clear(true);
            GameMesMananger.Instance().SetStage(2);
            SceneManager.LoadScene("stage_3");
            GameUIManager.updateUI();
            DOTween.Clear(true);
        }
    }
}