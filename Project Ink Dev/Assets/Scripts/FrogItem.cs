using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FrogItem : MonoBehaviour
{
    public GameObject PassLevelImage;

    void OnEnable()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    void LoadNewStage()
	{
        SceneManager.LoadScene("stage_2");
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
            Invoke("LoadNewStage", 8.0f);
            PassLevelImage.SetActive(true);
            StartCoroutine(Fade());
            GameUIManager.updateUI();
            DOTween.Clear(true);
        }
    }

    IEnumerator Fade()
	{
        float alpha = 0.0f;
        var img = PassLevelImage.GetComponent<CanvasGroup>();
        while(alpha<1.0f)
		{
            alpha += Time.deltaTime / 3.0f;
            img.alpha = alpha;
            yield return null;
		}
	}
}
