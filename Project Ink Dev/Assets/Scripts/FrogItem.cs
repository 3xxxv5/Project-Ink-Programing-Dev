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
      //PlayerPrefs.SetInt("PoMoLevel" + 1, 1);
      DOTween.Clear(true);
      GameMesMananger.Instance().save.hideCollections[0] = GameMesMananger.Instance().GetCurHiddenItemNum(GameMesMananger.Instance().getCurStageNum());
      GameMesMananger.Instance().SetStage(1);
      GameMesMananger.Instance().save.isLevelPass[1] = true;
      SaveManager.SaveByJSON(GameMesMananger.Instance().save);
      SceneManager.LoadSceneAsync("DialogStage1");
      GameUIManager.updateUI();
      DOTween.Clear(true);
    }
  }
}
