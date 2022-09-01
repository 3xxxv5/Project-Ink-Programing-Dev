using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FinalPassItem : MonoBehaviour
{
  public GameObject UICamera;
  
  void Start()
  {
    //GetComponent<BoxCollider>().isTrigger = true;
    DOTween.Clear(true);
    GameMesMananger.Instance().save.hideCollections[2] = GameMesMananger.Instance().GetCurHiddenItemNum(GameMesMananger.Instance().getCurStageNum());
    GameMesMananger.Instance().save.isLevelPass[3] = true;
    GameMesMananger.Instance().SetStage(-1);
    SaveManager.SaveByJSON(GameMesMananger.Instance().save);
    GameMesMananger.Instance().SetGameModeEnd();
    UICamera.SetActive(false);
    StartCoroutine(SlowlyLoadScene());
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

  private IEnumerator SlowlyLoadScene()
  {
    yield return new WaitForSeconds(2);
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;
    GameMesMananger.Instance().Clear();
    SceneManager.LoadSceneAsync("DialogStage3");
  }
}
