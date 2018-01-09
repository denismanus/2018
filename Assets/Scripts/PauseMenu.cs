using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private LevelGenetator levelGenetator;
    private LevelManager levelManager;
    public static bool isPaused = false;
    public GameObject UIpauseMenu;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelGenetator = FindObjectOfType<LevelGenetator>();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        isPaused = true;
        UIpauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        UIpauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        isPaused = false;
        UIpauseMenu.SetActive(false);
        levelManager.GetComponent<LevelManager>().ResetLevel();
        Time.timeScale = 1f;
    }
    //public void NextLevel()
    //{
    //    if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene("MainMenu");
    //    }
    //}

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
