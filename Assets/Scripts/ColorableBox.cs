
using UnityEngine;

public class ColorableBox : MonoBehaviour
{

    private enum Sides
    {
        top, right, bottom, left
    }
    public string typeOfTheBox;
    private string basicColor;
    public Transform positionOfTeleport;
    private GameObject levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>().gameObject;
        basicColor = typeOfTheBox;
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

    //Todo - исправить ебучее обнаружение  стороны и сделать нормльное отталкивание
    //Импуль по оси x НЕ РАБОТАЕТ, либо велосити с блокировкой управления надо пробовать
    //Либо отказаться от идеи откидывать в бок, хотя это хуёво
    //Надо исправить прыжки у стены
    void Push(GameObject gameObject)
    {
        Vector2 player = gameObject.GetComponent<Transform>().position;
        Vector2 block = this.GetComponent<Transform>().position;
        Vector2 push = new Vector2();
        Debug.Log(gameObject.GetComponent<Transform>().lossyScale);
        if (player.y > block.y )
        {
            if (player.x + 0.8f > block.x)
            {
                push = new Vector2(0, 9);
            }
        }
        else if (player.x < block.x)
        {
            push = new Vector2(-9, 0);
        }
        else if (player.x > block.x)
        {
            push = new Vector2(9, 0);
        }
        gameObject.GetComponent<TestScript>().Push(push);
    }

    private Sides GetSideCollision(GameObject gameObject)
    {
        Vector2 player = gameObject.GetComponent<Transform>().position;
        Vector2 block = this.GetComponent<Transform>().position;
        if (player.y > block.y-1f)
        {
            return Sides.top;
        }
        else if (player.x > block.x)
        {
            return Sides.right;
        }
        else if (player.x < block.x)
        {
            return Sides.left;
        }
        else
        {
            return Sides.bottom;
        }
    }
    //Sides side = GetSideCollision(gameObject);
    //    switch(side)
    //    {
    //        case Sides.bottom:
    //            break;
    //        case Sides.left:
    //            break;
    //    }
}
