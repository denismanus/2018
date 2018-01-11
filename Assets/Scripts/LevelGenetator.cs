using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenetator : MonoBehaviour {

    public Texture2D[] map;
    public ColorToPrefab[] colorToPrefab;
    private LevelManager levelManager;
    private int currentLevel = 5;
    private GameObject cameraScript;

	void Start () {
 
        levelManager = FindObjectOfType<LevelManager>();
        GenerateLevel();
        cameraScript = FindObjectOfType<CameraScript>().gameObject;
	}

    public void NextLevel()
    {
        if(currentLevel< map.Length)
        {
            currentLevel++;
            levelManager.DestroyObjects();
            GenerateLevel();
            cameraScript.GetComponent<CameraScript>().FindCharacter();
            Time.timeScale = 1f;
            //levelManager.ResetLevel();

        }
        else
        {
            Debug.Log("End");
        }
    }

    void GenerateLevel()
    {
        levelManager.ClearLevel();
        for (int x =0; x< map[currentLevel].width; x++)
        {
            for(int y = 0; y < map[currentLevel].height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        
        Color pixelColor = map[currentLevel].GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }
        foreach(ColorToPrefab colorMapping in colorToPrefab)
        {
            if(colorMapping.color.Equals(pixelColor))
            {
                Vector2 pos = new Vector2(x, y);
                levelManager.AddObj(Instantiate(colorMapping.prefab, pos, Quaternion.identity, transform));
            }
        }
    }
}
