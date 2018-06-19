using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelFromJson : MonoBehaviour
{

    private Level level;
    private Effects effects;
    private string[] levelsFromFolder;
    private Object[] allLevels;
    public StringToPrefab prefabs;
    private LevelManager levelManager;
    private GameObject cameraScript;
    private float[] cameraBoundaries = new float[4];

    void loadLevelsFromOuter()
    {
        string path = Directory.GetCurrentDirectory()+"/json";
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] filesinf = dir.GetFiles("*.json");
        levelsFromFolder = new string[filesinf.Length];
        int count = 0;
        foreach (FileInfo file in filesinf)
        {
            string contents = File.ReadAllText(file.FullName);
            levelsFromFolder[count] = contents;
            count++;
        }
    }
    void Start()
    {

        effects = FindObjectOfType<Effects>();
        loadLevelsFromOuter();
        //allLevels = Resources.LoadAll("Levels/Json");
        cameraScript = FindObjectOfType<CameraScript>().gameObject;
        levelManager = FindObjectOfType<LevelManager>();
        CurrentLevel();
    }


    public void GenerateLevel()
    {
        levelManager.ClearLevel();
        Vector2 pos;
        foreach (Block b in level.SimpleBlock)
        {
            pos = new Vector2(b.x, b.y);
            levelManager.AddWall(Instantiate(prefabs.GetPrefab("SimpleBlock"), pos, Quaternion.identity, transform));
        }
        foreach (Block b in level.Danger)
        {
            pos = new Vector2(b.x, b.y);
            levelManager.AddObj(Instantiate(prefabs.GetPrefab("Danger"), pos, Quaternion.identity, transform));
        }
        foreach (Block b in level.Saw)
        {
            pos = new Vector2(b.x, b.y);
            levelManager.AddObj(Instantiate(prefabs.GetPrefab("Saw"), pos, Quaternion.identity, transform));
        }
        foreach (ColorBlock b in level.Red)
        {
            pos = new Vector2(b.position.x, b.position.y);
            GameObject red = Instantiate(prefabs.GetPrefab("Red"), pos, Quaternion.identity, transform);
            levelManager.AddColorableBox(red);
            red.GetComponentInChildren<ColorableBox>().teleportCoord = b.teleportPosition;
        }
        foreach (ColorBlock b in level.Green)
        {
            pos = new Vector2(b.position.x, b.position.y);
            GameObject green = Instantiate(prefabs.GetPrefab("Green"), pos, Quaternion.identity, transform);
            levelManager.AddColorableBox(green);
            green.GetComponentInChildren<ColorableBox>().teleportCoord = b.teleportPosition;
        }
        foreach (ColorBlock b in level.Blue)
        {
            pos = new Vector2(b.position.x, b.position.y);
            GameObject blue = Instantiate(prefabs.GetPrefab("Blue"), pos, Quaternion.identity, transform);
            levelManager.AddColorableBox(blue);
            blue.GetComponentInChildren<ColorableBox>().teleportCoord = b.teleportPosition;
        }
        foreach (ColorBlock b in level.Purple)
        {
            pos = new Vector2(b.position.x, b.position.y);
            GameObject purple = Instantiate(prefabs.GetPrefab("Purple"), pos, Quaternion.identity, transform);
            levelManager.AddColorableBox(purple);
            purple.GetComponentInChildren<ColorableBox>().teleportCoord = b.teleportPosition;
        }
        foreach (ColorBlock b in level.Empty)
        {
            pos = new Vector2(b.position.x, b.position.y);
            GameObject empty = Instantiate(prefabs.GetPrefab("Empty"), pos, Quaternion.identity, transform);
            levelManager.AddColorableBox(empty);
            empty.GetComponentInChildren<ColorableBox>().teleportCoord = b.teleportPosition;
        }
        pos = new Vector2(level.Start.x, level.Start.y);
        levelManager.SetChar(Instantiate(prefabs.GetPrefab("Start"), pos, Quaternion.identity, transform));
        pos = new Vector2(level.Exit.x, level.Exit.y);
        levelManager.AddObj(Instantiate(prefabs.GetPrefab("Exit"), pos, Quaternion.identity, transform));
        levelManager.SetTeleports();
        //SetCameraBoundaries();
    }

    public void SetCameraBoundaries()
    {
        cameraBoundaries[0] = level.SimpleBlock[0].x;
        cameraBoundaries[1] = level.SimpleBlock[0].x;
        cameraBoundaries[2] = level.SimpleBlock[0].y;
        cameraBoundaries[3] = level.SimpleBlock[0].y;

        foreach (Block block in level.SimpleBlock)
        {
            if (block.x < cameraBoundaries[0])
            {
                cameraBoundaries[0] = block.x;
            }

            if (block.x > cameraBoundaries[1])
            {
                cameraBoundaries[1] = block.x;
            }

            if (block.y < cameraBoundaries[2])
            {
                cameraBoundaries[2] = block.y;
            }

            if (block.y > cameraBoundaries[3])
            {
                cameraBoundaries[3] = block.y;
            }
        }
        foreach (ColorBlock block in level.Blue)
        {
            if (block.position.x < cameraBoundaries[0])
            {
                cameraBoundaries[0] = block.position.x;
            }

            if (block.position.x > cameraBoundaries[1])
            {
                cameraBoundaries[1] = block.position.x;
            }

            if (block.position.y < cameraBoundaries[2])
            {
                cameraBoundaries[2] = block.position.y;
            }

            if (block.position.y > cameraBoundaries[3])
            {
                cameraBoundaries[3] = block.position.y;
            }
        }
        foreach (ColorBlock block in level.Green)
        {
            if (block.position.x < cameraBoundaries[0])
            {
                cameraBoundaries[0] = block.position.x;
            }

            if (block.position.x > cameraBoundaries[1])
            {
                cameraBoundaries[1] = block.position.x;
            }

            if (block.position.y < cameraBoundaries[2])
            {
                cameraBoundaries[2] = block.position.y;
            }

            if (block.position.y > cameraBoundaries[3])
            {
                cameraBoundaries[3] = block.position.y;
            }
        }
        foreach (ColorBlock block in level.Purple)
        {
            if (block.position.x < cameraBoundaries[0])
            {
                cameraBoundaries[0] = block.position.x;
            }

            if (block.position.x > cameraBoundaries[1])
            {
                cameraBoundaries[1] = block.position.x;
            }

            if (block.position.y < cameraBoundaries[2])
            {
                cameraBoundaries[2] = block.position.y;
            }

            if (block.position.y > cameraBoundaries[3])
            {
                cameraBoundaries[3] = block.position.y;
            }
        }
        foreach (ColorBlock block in level.Red)
        {
            if (block.position.x < cameraBoundaries[0])
            {
                cameraBoundaries[0] = block.position.x;
            }

            if (block.position.x > cameraBoundaries[1])
            {
                cameraBoundaries[1] = block.position.x;
            }

            if (block.position.y < cameraBoundaries[2])
            {
                cameraBoundaries[2] = block.position.y;
            }

            if (block.position.y > cameraBoundaries[3])
            {
                cameraBoundaries[3] = block.position.y;
            }
        }
        foreach (Block block in level.Danger)
        {
            if (block.x < cameraBoundaries[0])
            {
                cameraBoundaries[0] = block.x;
            }

            if (block.x > cameraBoundaries[1])
            {
                cameraBoundaries[1] = block.x;
            }

            if (block.y < cameraBoundaries[2])
            {
                cameraBoundaries[2] = block.y;
            }

            if (block.y > cameraBoundaries[3])
            {
                cameraBoundaries[3] = block.y;
            }
        }
        cameraScript.GetComponent<CameraScript>().SetBoundaries(cameraBoundaries[0], cameraBoundaries[1], cameraBoundaries[2], cameraBoundaries[3]);
    }


    public void CurrentLevel()
    {
        StaticData.MethodToDelegate fillLevel = LoadLevel;
        if (StaticData.currentLevel > PlayerPrefs.GetInt("CompletedLevel"))
        {
            PlayerPrefs.SetInt("CompletedLevel", StaticData.currentLevel);
        }
        StartCoroutine(FadeBerweenLevelLoads(fillLevel));
    }
    public void Nextlevel()
    {   
        StaticData.currentLevel++;
        CurrentLevel();
    }

   
    private void LoadLevel()
    {
        //if (StaticData.currentLevel < allLevels.Length)
        //{
        //    levelManager.DestroyObjects();
        //    level = JsonUtility.FromJson<Level>(allLevels[StaticData.currentLevel].ToString());
        //    GenerateLevel();
        //    cameraScript.GetComponent<CameraScript>().FindCharacter();
        //    cameraScript.GetComponent<CameraScript>().ChangeBackground();
        //    //SetCameraBoundaries();
        //    Time.timeScale = 1f;
        //}
        if (StaticData.currentLevel < levelsFromFolder.Length)
        {
            levelManager.DestroyObjects();
            level = JsonUtility.FromJson<Level>(levelsFromFolder[StaticData.currentLevel].ToString());
            GenerateLevel();
            cameraScript.GetComponent<CameraScript>().FindCharacter();
            cameraScript.GetComponent<CameraScript>().ChangeBackground();
            //SetCameraBoundaries();
            Time.timeScale = 1f;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator FadeBerweenLevelLoads(StaticData.MethodToDelegate method)
    {
        if (StaticData.isFirstLoadOfLevel)
        {
            method.Invoke();
            effects.FadeCurtain(true, 2, null);
            StaticData.isFirstLoadOfLevel = false;
        }
        else
        {
            effects.FadeCurtain(false, 1, method);
            yield return new WaitForSeconds(1);
            effects.FadeCurtain(true, 1, null);
        }
        
    }
}

[System.Serializable]
public class Level
{
    public Block[] SimpleBlock;
    public Block[] Danger;
    public Block[] Saw;
    public ColorBlock[] Red;
    public ColorBlock[] Green;
    public ColorBlock[] Blue;
    public ColorBlock[] Purple;
    public ColorBlock[] Empty;
    public Block Start;
    public Block Exit;
}

[System.Serializable]
public struct Block
{
    public int x, y;
}

[System.Serializable]
public struct ColorBlock
{
    public Block position;
    public Block teleportPosition;
}