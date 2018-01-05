using UnityEngine;

public class LevelGenetator : MonoBehaviour {

    public Texture2D map;
    public ColorToPrefab[] colorToPrefab;


	void Start () {
        GenerateLevel(); 
	}

    void GenerateLevel()
    {
        for(int x =0; x< map.width; x++)
        {
            for(int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }
        foreach(ColorToPrefab colorMapping in colorToPrefab)
        {
            if(colorMapping.color.Equals(pixelColor))
            {
                Vector2 pos = new Vector2(x, y);
                Instantiate(colorMapping.prefab, pos, Quaternion.identity, transform);
            }
        }
    }
	
}
