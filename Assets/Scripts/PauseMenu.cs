using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    private LevelManager levelManager;
    private AudioController audioController;
    private bool isLevelEnded = false;
    private LevelFromJson levelFromJson;
    public static bool isPaused = false;
    public GameObject UIpauseMenu;
    private GameObject menu;
    void Start()
    {
        levelFromJson = FindObjectOfType<LevelFromJson>();
        menu = GameObject.FindGameObjectWithTag("NextLevelMenu");
        menu.gameObject.SetActive(false);
        audioController = FindObjectOfType<AudioController>();
        levelManager = FindObjectOfType<LevelManager>();
        audioController.PlayFromBegin();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isLevelEnded)
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
    }

    void Pause()
    {
        isPaused = true;
        audioController.Pause();
        UIpauseMenu.transform.Find("Resume").GetComponent<Button>().Select();
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
        audioController.PlayFromBegin();
        UIpauseMenu.SetActive(false);
        levelManager.GetComponent<LevelManager>().ResetLevel();
        Time.timeScale = 1f;
    }

    public void NextLevelMenu()
    {
        
        isLevelEnded = true;
        menu.gameObject.SetActive(true);
        menu.transform.Find("LevelNumber").GetComponent<Text>().text = StaticData.currentLevel.ToString();
        menu.transform.Find("NextLevel").GetComponent<Button>().Select();
        Time.timeScale = 0f;
    }

    public void NextLevel()
    {
   
        isLevelEnded = false;
        menu.gameObject.SetActive(false);
        levelFromJson.Nextlevel();
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        audioController.Pause();
        isPaused = false;
        UIpauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
