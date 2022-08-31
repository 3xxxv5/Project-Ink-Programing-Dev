using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
  [SerializeField]
  private GameObject openingAnimation;
  [SerializeField]
  private Sprite[] openingSprites;
  [SerializeField]
  private Image image;
  [SerializeField]
  private GameObject startMenu;
  [SerializeField]
  private GameObject levelSlection;
  [SerializeField]
  private AudioSource player;

  private float oringalWidth;
  private float oringalHeight;

  private void Start()
  {
    // StartCoroutine(PlayOpeningAnimation());
    oringalWidth = image.rectTransform.sizeDelta.x;
    oringalHeight = image.rectTransform.sizeDelta.y;
  }
  private void EditImg(Image _img, Sprite _sprite)
  {
    _img.sprite = _sprite;
  }
  private IEnumerator MoveImgFromLeftToRight(Image _image)
  {
    float fromPos = 747.0f;
    float toPos = -747.0f;
    float speed = 500;
    while (fromPos > toPos)
    {
      fromPos -= speed * Time.deltaTime;
      _image.rectTransform.anchoredPosition3D = new Vector3(fromPos, 0, 0);
      yield return null;
    }
  }
  public void OpenStroy()
  {
    openingAnimation.SetActive(true);
    startMenu.SetActive(false);
    AudioClip clip = Resources.Load<AudioClip>("music/bgmDialog");
    player.clip = clip;
    player.volume = 0.2f;
    player.Play();
  }
  public void OpenLevelSlection()
  {
    openingAnimation.SetActive(false);
    startMenu.SetActive(false);
    levelSlection.SetActive(true);
  }
  public void OpenSettingMenu()
  {
    startMenu.SetActive(false);
    levelSlection.SetActive(false);
  }
  public void OpenStartMenu()
  {
    startMenu.SetActive(true);
    levelSlection.SetActive(false);
  }
  public void EndingGame()
  {
    //UnityEditor.EditorApplication.isPlaying = false;
    Application.Quit();
  }

}
