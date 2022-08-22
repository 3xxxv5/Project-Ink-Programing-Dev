using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FrogItem : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("PoMoLevel" + 1, 1);
            GameMesMananger.SetStage(1);
            DOTween.Clear(true);
            SceneManager.LoadScene("stage_2");
            DOTween.Clear(true);
        }
    }
}
