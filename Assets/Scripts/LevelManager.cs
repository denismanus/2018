using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public StringToSprite[] sprites;

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
}

[System.Serializable]
public class StringToSprite
{
    public string color;
    public Sprite sprite;
}
