using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public GameObject textImage;
    private Sprite[] levelImages;
    private FileInfo[] levelsFromFolder;
    private Toggle[] settings;
    Object[] levels;
    public GameObject buttonBackgrouned;


    void loadLevelsFromOuter()
    {
        string path = Directory.GetCurrentDirectory() + "/json";
        DirectoryInfo dir = new DirectoryInfo(path);
        levelsFromFolder = dir.GetFiles("*.json");
        //foreach (FileInfo file in filesinf)
        //{
        //    string contents = File.ReadAllText(file.FullName);
        //    levelsFromFolder[count] = contents;
        //    count++;
        //}
    }
    void Start()
    {
        loadLevelsFromOuter();
        SetSettings();
        effects = FindObjectOfType<Effects>();
        //StartCoroutine(ShowPreview());
        LoadTextLevels();
    }

    private void SetSettings()
    {
        settings = this.GetComponentsInChildren<Toggle>(true);
        foreach (Toggle toggle in settings)
        {
            if (toggle.name == "sound"&&toggle.isOn != StaticData.isSoundEnabled)
            {
                toggle.isOn = StaticData.isSoundEnabled;
                OffSound();
            }
            else if (toggle.name == "music"&&toggle.isOn != StaticData.isMusicEnabled)
            {
                toggle.isOn = StaticData.isMusicEnabled;
                OffMusic();
            }

        }
    }
    private void AddListenerToButtons()
    {

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
            yield return new WaitForSeconds(1);
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



    public void LoadTextLevels()
    {

        levelImages = Resources.LoadAll<Sprite>("Levels/Sprites");
        levels = Resources.LoadAll("Levels/Json");
        if (PlayerPrefs.HasKey("CompletedLevel"))
        {
            //for (int i = 0; i < levels.Length; i++)
            //{
            //    GameObject go = Instantiate(button);
            //    GameObject text = Instantiate(textImage);
            //    //pict.transform.SetParent(parent.GetComponent<Transform>());
            //    //pict.transform.SetAsLastSibling();
            //    go.transform.SetParent(parent.GetComponent<Transform>());
            //    go.transform.SetAsLastSibling();
            //    Button nbutton = go.GetComponent<Button>();
            //    nbutton.onClick.AddListener(() => SelectLevel());
            //    go.name = levels[i].name;
            //    Text buttonImage = text.GetComponent<Text>();
            //    buttonImage.text = i.ToString();
            //    buttonImage.transform.SetParent(go.GetComponent<Transform>());
            //}
            for (int i = 0; i < levelsFromFolder.Length; i++)
            {
                GameObject go = Instantiate(button);
                GameObject text = Instantiate(textImage);
                //pict.transform.SetParent(parent.GetComponent<Transform>());
                //pict.transform.SetAsLastSibling();
                go.transform.SetParent(parent.GetComponent<Transform>());
                go.transform.SetAsLastSibling();
                Button nbutton = go.GetComponent<Button>();
                nbutton.onClick.AddListener(() => SelectLevel());
                go.name = levelsFromFolder[i].Name;
                Text buttonImage = text.GetComponent<Text>();
                buttonImage.text = i.ToString();
                buttonImage.transform.SetParent(go.GetComponent<Transform>());
            }
        }
        else
        {
            PlayerPrefs.SetInt("CompletedLevel", 1);
            LoadTextLevels();
        }
    }
    public void LoadSpritesLevels()
    {
        //Чтоб этот метод заработал надо в префаб кнопки добавить компонент изображение
        levelImages = Resources.LoadAll<Sprite>("Levels/Sprites");
        levels = Resources.LoadAll("Levels/Json");
        if (PlayerPrefs.HasKey("CompletedLevel"))
        {
            for (int i = 0; i < levels.Length; i++)
            {
                //if (i <= PlayerPrefs.GetInt("CompletedLevel"))
                //{
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
        }
        else
        {
            PlayerPrefs.SetInt("CompletedLevel", 1);
        }

    }

    public void LoadLevels()
    {
        
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
        for (int i = 0; i < levelsFromFolder.Length; i++)
        {
            if (levelsFromFolder[i].Name.Equals(EventSystem.current.currentSelectedGameObject.name))
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
        StaticData.isSoundEnabled = !StaticData.isSoundEnabled;
    }
    public void OffMusic()
    {
        StaticData.isMusicEnabled = !StaticData.isMusicEnabled; 
    }
}
