using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public StringToSprite[] sprites;
    private List<GameObject> objectList = new List<GameObject>();
    private List<GameObject> colorableBox = new List<GameObject>();
    public GameObject backGround;


    void Start()
    {
    }

    public void SetTeleports()
    {
       foreach(GameObject a in colorableBox)
        {
            foreach(GameObject b in colorableBox)
            {
                if (!a.Equals(b))
                {
                    if (a.GetComponent<ColorableBox>().teleportCoord.x
                    == (int)b.GetComponent<Transform>().position.x && a.GetComponent<ColorableBox>().teleportCoord.y
                    == (int)b.GetComponent<Transform>().position.y)
                    {
                        a.GetComponent<ColorableBox>().teleport = b;
                        break;
                    }
                }
            }
        }
    }

    public bool FindBlockInPosition(Vector2 vect)
    {
        Debug.Log(vect);
        foreach (GameObject wall in objectList)
        {
            if(wall.name == "SimpleBlock(Clone)")
            {
                
                if(wall.GetComponent<Transform>().position.x== vect.x && wall.GetComponent<Transform>().position.y == vect.y)
                {
                    
                    return false;
                }
            }
        }
        return true;
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
        foreach (GameObject box in objectList)
        {
            if (box.gameObject.GetComponent<TestScript>())
            {
                box.gameObject.GetComponent<TestScript>().Reset();
            }
        }
        foreach (GameObject box in colorableBox)
        {
            box.GetComponent<ColorableBox>().Reset();
        }
    }
    public void DestroyObjects()
    {
        foreach (GameObject box in objectList)
        {
            Destroy(box);
        }
        foreach (GameObject box in colorableBox)
        {
            Destroy(box);
        }
    }
}

[System.Serializable]
public class StringToSprite
{
    public string color;
    public Sprite sprite;
}
