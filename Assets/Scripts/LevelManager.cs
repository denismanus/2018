using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public StringToSprite[] sprites;
    private List<GameObject> objectList = new List<GameObject>();
    public GameObject backGround; 


    void Start()
    {
    }

    public Sprite GetSprite(string name)
    {
        foreach(StringToSprite a in sprites)
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
    }
    public void AddObj(GameObject obj)
    {
        objectList.Add(obj);
    }

    public void ResetLevel()
    {
        foreach(GameObject box in objectList)
        {
            if (box.gameObject.GetComponent<ColorableBox>())
            {
                box.gameObject.GetComponent<ColorableBox>().Reset();
            }
            else if (box.gameObject.GetComponent<TestScript>())
            {
                box.gameObject.GetComponent<TestScript>().Reset();
            }
        }
    }
    public void DestroyObjects()
    {
        foreach (GameObject box in objectList)
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
