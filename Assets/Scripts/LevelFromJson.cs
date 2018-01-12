using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LevelFromJson : MonoBehaviour
{

    private Level level;
    private Object[] allLevels;
    public StringToPrefab prefabs;
    private LevelManager levelManager;
    private GameObject cameraScript;


    void Start()
    {
        allLevels = Resources.LoadAll("Levels");
        cameraScript = FindObjectOfType<CameraScript>().gameObject;
        levelManager = FindObjectOfType<LevelManager>();
        level = JsonUtility.FromJson<Level>(allLevels[StaticData.currentLevel].ToString());
        GenerateLevel();
    }


    public void GenerateLevel()
    {
        levelManager.ClearLevel();
        Vector2 pos;
        foreach (Block b in level.SimpleBlock)
        {
            pos = new Vector2(b.x, b.y);
            levelManager.AddObj(Instantiate(prefabs.GetPrefab("SimpleBlock"), pos, Quaternion.identity, transform));
        }
        foreach (Block b in level.Danger)
        {
            pos = new Vector2(b.x, b.y);
            levelManager.AddObj(Instantiate(prefabs.GetPrefab("Danger"), pos, Quaternion.identity, transform));
        }
        foreach (ColorBlock b in level.Red)
        {
            pos = new Vector2(b.position.x, b.position.y);
            levelManager.AddColorableBox(Instantiate(prefabs.GetPrefab("Red"), pos, Quaternion.identity, transform));
        }
        foreach (ColorBlock b in level.Green)
        {
            pos = new Vector2(b.position.x, b.position.y);
            GameObject green = Instantiate(prefabs.GetPrefab("Green"), pos, Quaternion.identity, transform);
            levelManager.AddColorableBox(green);
            green.GetComponent<ColorableBox>().teleportCoord = b.teleportPosition;
        }
        foreach (ColorBlock b in level.Blue)
        {
            pos = new Vector2(b.position.x, b.position.y);
            levelManager.AddColorableBox(Instantiate(prefabs.GetPrefab("Blue"), pos, Quaternion.identity, transform));
        }
        foreach (ColorBlock b in level.Purple)
        {
            pos = new Vector2(b.position.x, b.position.y);
            levelManager.AddColorableBox(Instantiate(prefabs.GetPrefab("Purple"), pos, Quaternion.identity, transform));
        }
        pos = new Vector2(level.Start.x, level.Start.y);
        levelManager.AddObj(Instantiate(prefabs.GetPrefab("Start"), pos, Quaternion.identity, transform));
        pos = new Vector2(level.Exit.x, level.Exit.y);
        levelManager.AddObj(Instantiate(prefabs.GetPrefab("Exit"), pos, Quaternion.identity, transform));
        levelManager.SetTeleports();
    }
    public void SelectLevel(int i)
    {
        StaticData.currentLevel = i;
        levelManager.DestroyObjects();
        GenerateLevel();
    }
    public void nextlevel()
    {
        if (StaticData.currentLevel < allLevels.Length)
        {
            StaticData.currentLevel++;
            levelManager.DestroyObjects();
            level = JsonUtility.FromJson<Level>(allLevels[StaticData.currentLevel].ToString());
            GenerateLevel();
            cameraScript.GetComponent<CameraScript>().FindCharacter();
            Time.timeScale= 1f;
            //levelmanager.resetlevel();
        }
        else
        {
            Debug.Log("end");
        }
    }
}

[System.Serializable]
public class Level
{
    public Block[] SimpleBlock;
    public Block[] Danger;
    public ColorBlock[] Red;
    public ColorBlock[] Green;
    public ColorBlock[] Blue;
    public ColorBlock[] Purple;
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