using UnityEngine;

public class EndGame : MonoBehaviour
{
  public void EndingGame()
  {
    UnityEditor.EditorApplication.isPlaying = false;
    Application.Quit();
  }
}
