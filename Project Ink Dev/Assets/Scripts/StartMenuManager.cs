using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
  [SerializeField]
  private GameObject startMenu;
  [SerializeField]
  private GameObject levelSlection;
  [SerializeField]
  private GameObject settingMenu;


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
    UnityEditor.EditorApplication.isPlaying = false;
    Application.Quit();
  }
}
