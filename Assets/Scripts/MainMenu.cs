using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject button;
    public GameObject parent;
    Object[] levels;
    void Start () {
        LoadLevels();
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevels()
    {
        levels = Resources.LoadAll("Levels");
        for(int i = 0; i < levels.Length; i++)
        {
            GameObject go = Instantiate(button);
            go.transform.SetParent(parent.GetComponent<Transform>());
            go.transform.SetAsFirstSibling();
            Button nbutton = go.GetComponent<Button>();
            nbutton.onClick.AddListener(() => SelectLevel());
            go.name = levels[i].name;
            nbutton.GetComponentInChildren<Text>().text = levels[i].name;
        }
    }

    public void SelectLevel()
    {
        for(int i = 0; i < levels.Length; i++)
        {
            if (levels[i].name.Equals(EventSystem.current.currentSelectedGameObject.name))
            {
                StaticData.currentLevel = i;
                PlayGame();
            }
        }
    }
}
