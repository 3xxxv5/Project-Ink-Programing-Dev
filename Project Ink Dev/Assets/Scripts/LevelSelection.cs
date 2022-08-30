using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSelection : MonoBehaviour
{
  [SerializeField] private bool m_unlock = false; // 默认为关卡锁定
  private Button m_button;
  private Image m_image;
  private int stageIndex;

  private void Start()
  {
    m_button = gameObject.GetComponent<Button>();
    m_image = gameObject.GetComponent<Image>();
    stageIndex = int.Parse(m_button.gameObject.name);
  }

  private void Update()
  {
    UpdateLevelStatus();
  }

  private void UpdateLevelStatus()
  {
    if (GameMesMananger.Instance().save != null && GameMesMananger.Instance().save.isLevelPass.Count >= stageIndex)
    {
      if (GameMesMananger.Instance().save.isLevelPass[stageIndex - 1] == true)
        this.m_unlock = true;
    }

    if (this.m_unlock == false)
    {
      m_button.enabled = false;
      m_image.color = Color.gray;
    }
    else
    {
      m_button.enabled = true;
      m_image.color = Color.white;
    }
  }

  public void PressSelection(string sceneName)
  {
    if (m_unlock == true)
    {
      if (sceneName == "SampleScene2")
      {
        GameMesMananger.Instance().SetStage(0);
      }
      else if (sceneName == "stage_2")
      {
        GameMesMananger.Instance().SetStage(1);
      }
      else if (sceneName == "stage_3")
      {
        GameMesMananger.Instance().SetStage(2);
      }
      SceneManager.LoadSceneAsync(sceneName);
      //GameUIManager.updateUI();
      DOTween.Clear(true);
    }
  }
}
