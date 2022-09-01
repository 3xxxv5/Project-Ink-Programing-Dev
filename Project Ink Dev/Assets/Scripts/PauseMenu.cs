using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class PauseMenu : MonoBehaviour
{
  public static bool GameIsPause = false;
  public GameObject pauseMenuUI;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (GameIsPause)
      {
        Resume();
      }
      else
      {
        Pause();
      }
    }
  }

  public void Resume()
  {
    pauseMenuUI.SetActive(false);
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
    Time.timeScale = 1.0f;
    GameIsPause = false;
  }

  public void Pause()
  {
    pauseMenuUI.SetActive(true);
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;
    Time.timeScale = 0.0f;
    GameIsPause = true;
  }

  public void MainMenu()
  {
    GameIsPause = false;
    Time.timeScale = 1.0f;
        //SaveManager.SaveByJSON(GameMesMananger.Instance().save);
    GameMesMananger.Instance().SetStage(-1);
    SceneManager.LoadSceneAsync("StartMenu");

    GameMesMananger.Instance().save = SaveManager.LoadByJSON();
    GameMesMananger.Instance().Clear();
  }

  public void ReSet()
  {
    DOTween.Clear(true);
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    GameMesMananger.Instance().Clear();
    Resume();
  }
}
