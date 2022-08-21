using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
  [SerializeField]
  private GameObject startMenu;
  [SerializeField]
  private GameObject levelSlection;
  public void LoadingGame()
  {
    startMenu.SetActive(false);
    levelSlection.SetActive(true);
  }
}
