using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private LevelManager levelManager;
    private AudioController audioController;
    public static bool isPaused = false;
    public GameObject UIpauseMenu;

    void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        levelManager = FindObjectOfType<LevelManager>();
        audioController.PlayFromBegin();
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
        audioController.Pause();
        UIpauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        audioController.Play();
        isPaused = false;
        UIpauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        isPaused = false;
        audioController.Play();
        UIpauseMenu.SetActive(false);
        levelManager.GetComponent<LevelManager>().ResetLevel();
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        isPaused = false;
        UIpauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
