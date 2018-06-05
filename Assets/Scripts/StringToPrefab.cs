using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringToPrefab : MonoBehaviour
{

    public PrefabName[] allPrefs;
    void Start()
    {

    }
    public GameObject GetPrefab(string name)
    {
        foreach(PrefabName pref in allPrefs)
        {
            if(pref.type.Equals(name))
            {
                return pref.gameObject;
            }
        }
        return null;
    }
}

[System.Serializable]
public class PrefabName
{
    public string type;
    public GameObject gameObject;
}