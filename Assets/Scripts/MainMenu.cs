using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool isLoaded = true;
    private float timeTo = 5f;
    private Effects effects;
    public GameObject button;
    public GameObject blockedButton;
    public GameObject parent;
    public GameObject backGround;
    public GameObject gaminator;
    public GameObject menu;
    private Sprite[] levelImages;
    Object[] levels;


    void Start()
    {
        effects = FindObjectOfType<Effects>();
        StartCoroutine(ShowPreview());
        LoadSpritesLevels();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ShowPreview()
    {
        if (!StaticData.isLogoShown)
        {
            effects.FadeCurtain(true, 1, null);
            yield return new WaitForSeconds(2);
            effects.FadeCurtain(false, 1, null);
            yield return new WaitForSeconds(1);
            gaminator.SetActive(false);
            effects.FadeCurtain(true, 1, null);
            backGround.SetActive(true);
            menu.SetActive(true);
            effects.FadeCurtain(true, 1, null);
            StaticData.isLogoShown = true;
        }
        else
        {
            effects.FadeCurtain(true, 1, null);
            gaminator.SetActive(false);
            effects.FadeCurtain(true, 1, null);
            backGround.SetActive(true);
            menu.SetActive(true);
            effects.FadeCurtain(true, 1, null);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }




    public void LoadSpritesLevels()
    {
        levelImages = Resources.LoadAll<Sprite>("Levels/Sprites");
        levels = Resources.LoadAll("Levels/Json");
        if (PlayerPrefs.HasKey("CompletedLevel"))
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("CompletedLevel"))
                {
                    GameObject go = Instantiate(button);
                    go.transform.SetParent(parent.GetComponent<Transform>());
                    go.transform.SetAsLastSibling();
                    Button nbutton = go.GetComponent<Button>();
                    nbutton.onClick.AddListener(() => SelectLevel());
                    go.name = levels[i].name;
                    Image buttonImage = go.GetComponent<Image>();
                    if (i < levelImages.Length)
                    {
                        buttonImage.sprite = levelImages[i];
                    }
                    else
                    {
                        buttonImage.sprite = levelImages[0];
                    }
                }
                else
                {
                    GameObject go = Instantiate(blockedButton);
                    go.GetComponentInChildren<Text>().text = levels[i].name;
                    go.transform.SetParent(parent.GetComponent<Transform>());
                    go.transform.SetAsLastSibling();
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("CompletedLevel", 1);
        }

    }

    public void LoadLevels()
    {

        Debug.Log(PlayerPrefs.GetInt("CompletedLevel"));
        levels = Resources.LoadAll("Levels/Json");
        for (int i = 0; i < levels.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt("CompletedLevel"))
            {
                GameObject go = Instantiate(button);
                go.transform.SetParent(parent.GetComponent<Transform>());
                go.transform.SetAsFirstSibling();
                Button nbutton = go.GetComponent<Button>();
                nbutton.onClick.AddListener(() => SelectLevel());
                go.name = levels[i].name;
                nbutton.GetComponentInChildren<Text>().text = levels[i].name;
            }
            else
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
    }

    public void SelectLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].name.Equals(EventSystem.current.currentSelectedGameObject.name))
            {
                StaticData.MethodToDelegate start = PlayGame;
                StaticData.currentLevel = i;
                StaticData.isFirstLoadOfLevel = true;
                effects.FadeCurtain(false, 1, start);
            }
        }
    }

    public void OffSound()
    {
        StaticData.isSounded = !StaticData.isSounded;
    }
}
