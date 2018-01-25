using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public StringToSprite[] sprites;
    private List<GameObject> objectList = new List<GameObject>();
    private List<GameObject> walls = new List<GameObject>();
    private GameObject character;
    private List<GameObject> colorableBox = new List<GameObject>();
    public GameObject backGround;


    void Start()
    {
    }

    public void SetChar(GameObject character)
    {
        this.character = character;
    }
    public void AddWall(GameObject wall)
    {
        walls.Add(wall);
    }

    public void SetTeleports()
    {
        foreach (GameObject a in colorableBox)
        {
            foreach (GameObject b in colorableBox)
            {
                if (!a.Equals(b))
                {
                    if (a.GetComponentInChildren<ColorableBox>().teleportCoord.x
                    == (int)b.GetComponentInChildren<Transform>().position.x && a.GetComponentInChildren<ColorableBox>().teleportCoord.y
                    == (int)b.GetComponentInChildren<Transform>().position.y)
                    {
                        a.GetComponentInChildren<ColorableBox>().teleport = b;
                        break;
                    }
                }
            }
        }
    }

    public bool FindBlockInPosition(Vector2 vect)
    {
        foreach (GameObject wall in walls)
        {
            if (wall.GetComponent<Transform>().position.x == vect.x && wall.GetComponent<Transform>().position.y == vect.y)
            {

                return false;
            }
        }
        return true;
    }

    public void ChangeBlueTrigger(bool value)
    {
        foreach (GameObject box in colorableBox)
        {
            if (box.GetComponentInChildren<ColorableBox>().typeOfTheBox == "Blue")
            {
                box.GetComponentInChildren<ColorableBox>().SetTriggers(value);
            }
        }
    }

    public Sprite GetSprite(string name)
    {
        foreach (StringToSprite a in sprites)
        {
            if (a.color.Equals(name))
            {
                return a.sprite;
            }
        }
        return null;
    }

    public void ClearLevel()
    {
        objectList.Clear();
        colorableBox.Clear();
        walls.Clear();
        character = null;
    }
    public void AddObj(GameObject obj)
    {
        objectList.Add(obj);
    }
    public void AddColorableBox(GameObject obj)
    {
        colorableBox.Add(obj);
    }
    public void ResetLevel()
    {

        character.GetComponent<TestScript>().Reset();
        foreach (GameObject box in colorableBox)
        {
            box.GetComponentInChildren<ColorableBox>().Reset();
        }
    }

    public void DestroyObjects()
    {
        foreach (GameObject wall in walls)
        {
            Destroy(wall);
        }
        foreach (GameObject box in objectList)
        {
            Destroy(box);
        }
        foreach (GameObject box in colorableBox)
        {
            Destroy(box);
        }
        Destroy(character);
    }
}

[System.Serializable]
public class StringToSprite
{
    public string color;
    public Sprite sprite;
}
