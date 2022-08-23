using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSelection : MonoBehaviour
{
  [SerializeField] private bool m_unlock = false; // 默认为关卡锁定
  private Button m_button;
  private Image m_image;
  private void Start()
  {
    m_button = gameObject.GetComponent<Button>();
    m_image = gameObject.GetComponent<Image>();
  }
  private void Update()
  {
    UpdateLevelStatus();
  }
  private void UpdateLevelStatus()
  {
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
      SceneManager.LoadScene(sceneName);
      DOTween.Clear(true);
    }
  }
}
