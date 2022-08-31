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
      GameMesMananger.Instance().save.hideCollections[1] = GameMesMananger.Instance().GetCurHiddenItemNum(GameMesMananger.Instance().getCurStageNum());
      GameMesMananger.Instance().save.isLevelPass[2] = true;
      GameMesMananger.Instance().SetStage(2);
      SaveManager.SaveByJSON(GameMesMananger.Instance().save);
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
      SceneManager.LoadSceneAsync("DialogStage2");
      GameUIManager.updateUI();
      DOTween.Clear(true);
    }
  }
}
