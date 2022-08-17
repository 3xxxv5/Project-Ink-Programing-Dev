using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterface : MonoBehaviour
{
  private TextMeshProUGUI CurrentScore;
  private TextMeshProUGUI TargetScore;

  private void Start()
  {
        CurrentScore = GameObject.Find("Canvas/Text[CurrentScore]").GetComponent<TextMeshProUGUI>();
        TargetScore = GameObject.Find("Canvas/Text[TargetScore]").GetComponent<TextMeshProUGUI>();
        RefreshScore(0, 15);
  }
  // private void Update()
  // {
  //   RefreshScore(1, 2);
  // }
  public void RefreshScore(int cScore, int tScore)
  {
    CurrentScore.text = "Current:" + cScore.ToString();
    TargetScore.text = "total:" + tScore.ToString();
  }
}
