
using UnityEngine;

public class ColorableBox : MonoBehaviour
{

    private enum Sides
    {
        top, right, bottom, left
    }
    private Sides actualSide;
    public string typeOfTheBox;
    private string basicColor;
    public Transform positionOfTeleport;
    private GameObject levelManager;

    void Start()
    {
        basicColor = typeOfTheBox;
        levelManager = FindObjectOfType<LevelManager>().gameObject;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            ColorCheck(coll.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            ColorCheck(coll.gameObject);
        }
        else if (coll.gameObject.tag == "Side")
        {
            GetSideCollision(coll);
        }
    }
    void ColorCheck(GameObject gameObject)
    {

        GetComponent<BoxCollider2D>().isTrigger = false;
        string charType = gameObject.GetComponent<TestScript>().GetTypeOfCube();
        if (charType != typeOfTheBox)
        {
            ExchangeColor(gameObject, charType);
        }
        else if (charType == "Blue" && typeOfTheBox == "Blue")
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (charType == "Red")
        {
            Push(gameObject);
        }
        else if (charType == "Green")
        {
            TeleportTo(gameObject);
        }
    }


    public void Reset()
    {
        typeOfTheBox = basicColor;
        Debug.Log(basicColor);
        Debug.Log(typeOfTheBox);
        Debug.Log(levelManager);
        Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite);
        gameObject.GetComponent<SpriteRenderer>().sprite = levelManager.GetComponent<LevelManager>().GetSprite(typeOfTheBox);
    }
    

    void ExchangeColor(GameObject gameObject, string charType)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = levelManager.GetComponent<LevelManager>().GetSprite(charType);
        gameObject.GetComponent<SpriteRenderer>().sprite = levelManager.GetComponent<LevelManager>().GetSprite(typeOfTheBox + "Char");
        gameObject.GetComponent<TestScript>().SetTypeOfCube(typeOfTheBox);
        typeOfTheBox = charType;
    }

    void TeleportTo(GameObject gameObject)
    {
        if (positionOfTeleport != null)
        {
            gameObject.GetComponent<Transform>().position = new Vector2(positionOfTeleport.position.x, positionOfTeleport.position.y + positionOfTeleport.localScale.y);
        }
    }

    void Push(GameObject gameObject)
    {
        Debug.Log(actualSide);
        Vector2 player = gameObject.GetComponent<Transform>().position;
        Vector2 block = this.GetComponent<Transform>().position;
        Vector2 push = new Vector2();
        if (actualSide == Sides.bottom)
        {
            push = new Vector2(0, 11);
        }
        else if (actualSide == Sides.left)
        {
            //push = new Vector2(8, 3);
        }
        else if (actualSide == Sides.right)
        {
            //push = new Vector2(-8, 3);
        }
        gameObject.GetComponent<TestScript>().Push(push);
    }

    private void GetSideCollision(Collider2D gameObject)
    {
        switch (gameObject.name)
        {
            case "Left":
                actualSide = Sides.left;
                break;
            case "Bottom":
                actualSide = Sides.bottom;
                break;
            case "Right":
                actualSide = Sides.right;
                break;
            case "Top":
                actualSide = Sides.top;
                break;
        }
    }
}
