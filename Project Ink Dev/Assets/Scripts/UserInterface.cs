using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections;

public class UserInterface : MonoBehaviour
{
  private TextMeshProUGUI CurrentScore;
  private TextMeshProUGUI TargetScore;
  private GameObject[] ImageList;
  private Sprite collectionFull;
  private Sprite collectionEmpty;

  private void Awake()
  {
    collectionFull = Resources.Load<Sprite>("Image/collection_full");
    collectionEmpty = Resources.Load<Sprite>("Image/collection_empty");
    ImageList = GameObject.FindGameObjectsWithTag("Collections");
    ImageList = ImageList.OrderBy(go => go.transform.GetSiblingIndex()).ToArray();
  }

  private void Start()
  {
    foreach (GameObject image in ImageList)
    {
      //Debug.Log("test");
      image.GetComponent<Image>().sprite = collectionEmpty;
    }
  }
  public void RefreshScore(int cScore, int tScore)
  {
    if (cScore == 0 || cScore > tScore)
      return;
    ImageList[cScore - 1].GetComponent<Image>().sprite = collectionFull;
    StartCoroutine(CollectionsEffect(ImageList[cScore - 1].transform));
  }

  public void RefreshHideScore(int hideScore, int tHideScore)
  {
    if (hideScore == 0 || hideScore > tHideScore)
      return;
    int length = ImageList.Length;
    Image nextEmptyImg = ImageList[length - tHideScore + hideScore - 1].GetComponent<Image>();
    nextEmptyImg.sprite = collectionFull;
    Color hideCollectionsColor = new Color(101f / 255, 168f / 255, 1, 1);
    if (nextEmptyImg.color == hideCollectionsColor)
    {
      return;
    }
    nextEmptyImg.color = hideCollectionsColor;
    StartCoroutine(CollectionsEffect(nextEmptyImg.transform));
  }
  private IEnumerator CollectionsEffect(Transform _tf)
  {
    float scale = 0.3f;
    float min = scale;
    float max = scale * 2;
    while (scale < max)
    {
      scale += Time.fixedDeltaTime * 2;
      _tf.transform.localScale = new Vector3(scale, scale, scale);
      yield return null;
    }
    while (scale > min)
    {
      scale -= Time.fixedDeltaTime * 2;
      _tf.transform.localScale = new Vector3(scale, scale, scale);
      yield return null;
    }
    // Debug.Log("end coroutine");
  }
}
