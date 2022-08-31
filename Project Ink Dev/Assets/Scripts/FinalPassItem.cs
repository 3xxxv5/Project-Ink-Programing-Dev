using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FinalPassItem : MonoBehaviour
{
  void Start()
  {
    //GetComponent<BoxCollider>().isTrigger = true;
        DOTween.Clear(true);
        GameMesMananger.Instance().save.hideCollections[2] = GameMesMananger.Instance().GetCurHiddenItemNum(GameMesMananger.Instance().getCurStageNum());
        GameMesMananger.Instance().save.isLevelPass[3] = true;
        GameMesMananger.Instance().SetStage(-1);
        SaveManager.SaveByJSON(GameMesMananger.Instance().save);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadSceneAsync("DialogStage3");
        GameMesMananger.Instance().save = SaveManager.LoadByJSON();
        //GameUIManager.updateUI();
        DOTween.Clear(true);
    }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      //PlayerPrefs.SetInt("PoMoLevel" + 1, 1);

    }
  }
}
