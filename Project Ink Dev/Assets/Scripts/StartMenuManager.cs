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
  private GameObject settingMenu;
  [SerializeField]
  private CanvasGroup fade;
  [SerializeField]
  private float fadeDuration;
  private bool isFade;

  private float oringalWidth;
  private float oringalHeight;

  private void Start()
  {
    StartCoroutine(PlayOpeningAnimation());
    oringalWidth = image.rectTransform.sizeDelta.x;
    oringalHeight = image.rectTransform.sizeDelta.y;
  }
  private void EditImg(Image _img, Sprite _sprite)
  {
    _img.sprite = _sprite;
  }
  private IEnumerator PlayOpeningAnimation()
  {
    EditImg(image, openingSprites[0]);
    yield return Fade(0);
    yield return new WaitForSecondsRealtime(4.0f);

    yield return Fade(1);
    EditImg(image, openingSprites[1]);
    yield return Fade(0);
    yield return new WaitForSecondsRealtime(4.0f);

    yield return Fade(1);
    EditImg(image, openingSprites[2]);
    image.rectTransform.sizeDelta = new Vector2(3415, 1080);
    StartCoroutine(MoveImgFromLeftToRight(image));
    yield return Fade(0);
    yield return new WaitForSecondsRealtime(4.0f);

    yield return Fade(1);
    image.rectTransform.sizeDelta = new Vector2(oringalWidth, oringalHeight);
    image.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
    EditImg(image, openingSprites[3]);
    yield return Fade(0);
    yield return new WaitForSecondsRealtime(4.0f);

    yield return Fade(1);
    SkipAnimation();
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
  public void OpenLevelSlection()
  {
    startMenu.SetActive(false);
    levelSlection.SetActive(true);
    settingMenu.SetActive(false);
  }
  public void OpenSettingMenu()
  {
    startMenu.SetActive(false);
    levelSlection.SetActive(false);
    settingMenu.SetActive(true);
  }
  public void OpenStartMenu()
  {
    startMenu.SetActive(true);
    settingMenu.SetActive(false);
    levelSlection.SetActive(false);
  }
  public void EndingGame()
  {
    //UnityEditor.EditorApplication.isPlaying = false;
    Application.Quit();
  }
  public void SkipAnimation()
  {
    StopAllCoroutines();
    StartCoroutine(Fade(1));
    openingAnimation.SetActive(false);
    startMenu.SetActive(true);
    fade.gameObject.SetActive(false);
  }

  private IEnumerator Fade(float targetAlpha)
  {
    isFade = true;

    fade.blocksRaycasts = true;

    float speed = Mathf.Abs(fade.alpha - targetAlpha) / fadeDuration;

    while (!Mathf.Approximately(fade.alpha, targetAlpha))
    {
      fade.alpha = Mathf.MoveTowards(fade.alpha, targetAlpha, speed * Time.deltaTime);
      yield return null;
    }
    fade.blocksRaycasts = false;

    isFade = false;
  }
}
