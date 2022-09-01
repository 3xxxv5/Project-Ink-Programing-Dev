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
    StartCoroutine(SetCollectionImg(ImageList, cScore - 1, false, collectionFull));
    // ImageList[cScore - 1].GetComponent<Image>().sprite = collectionFull;
    // StartCoroutine(CollectionsEffect(ImageList[cScore - 1].transform));
  }
  private IEnumerator SetCollectionImg(GameObject[] _imgList, int _offset, bool _isHide, Sprite _collectionFull) {
    int length = 0;
    while (length == 0) {
      length = ImageList.Length;
    }
    Image img = _isHide ? ImageList[length + _offset].GetComponent<Image>() : ImageList[_offset].GetComponent<Image>();
    img.sprite = _collectionFull;
    Color hideCollectionsColor = new Color(101f / 255, 168f / 255, 1, 1);
    if (img.color == hideCollectionsColor) {
      yield break;
    }
    if (_isHide) {
    img.color = hideCollectionsColor;
    }
    StartCoroutine(CollectionsEffect(img.transform));
  }

  public void RefreshHideScore(int hideScore, int tHideScore)
  {
    if (hideScore == 0 || hideScore > tHideScore)
      return;
    StartCoroutine(SetCollectionImg(ImageList, - tHideScore + hideScore - 1, true, collectionFull));
    // int length = ImageList.Length;
    
    // Image nextEmptyImg = ImageList[length - tHideScore + hideScore - 1].GetComponent<Image>();
    // nextEmptyImg.sprite = collectionFull;
    // Color hideCollectionsColor = new Color(101f / 255, 168f / 255, 1, 1);
    // if (nextEmptyImg.color == hideCollectionsColor)
    // {
    //   return;
    // }
    // nextEmptyImg.color = hideCollectionsColor;
    // StartCoroutine(CollectionsEffect(nextEmptyImg.transform));
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
