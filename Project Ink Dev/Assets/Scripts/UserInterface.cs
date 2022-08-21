using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterface : MonoBehaviour
{
  private TextMeshProUGUI CurrentScore;
  private TextMeshProUGUI TargetScore;
  private GameObject[] ImageList;
  private Sprite collectionFull;
  private Sprite collectionEmpty;

  private void Awake()
  {
    CurrentScore = GameObject.Find("Canvas/Text[CurrentScore]").GetComponent<TextMeshProUGUI>();
    TargetScore = GameObject.Find("Canvas/Text[TargetScore]").GetComponent<TextMeshProUGUI>();
    collectionFull = Resources.Load<Sprite>("Image/collection_full");
    collectionEmpty = Resources.Load<Sprite>("Image/collection_empty");
    ImageList = GameObject.FindGameObjectsWithTag("Collections");
  }
  private void Start()
  {
    foreach (GameObject image in ImageList)
    {
      Debug.Log("test");
      image.GetComponent<Image>().sprite = collectionEmpty;
    }
  }
  // private void Update()
  // {
  //   RefreshScore(1, 2);
  // }
  public void RefreshScore(int cScore, int tScore)
  {
    CurrentScore.text = "Current:" + cScore.ToString();
    TargetScore.text = "total:" + tScore.ToString();
    ImageList[cScore - 1].GetComponent<Image>().sprite = collectionFull;
  }
}
