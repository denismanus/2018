
using UnityEngine;

public class ColorableBox : MonoBehaviour
{

    private enum Sides
    {
        top, right, bottom, left
    }
    private bool isCollisionActiv = true;
    public Block teleportCoord;
    private Sides actualSide;
    public string typeOfTheBox;
    private string basicColor;
    public GameObject teleport;
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
            if (isCollisionActiv)
            {
                ColorCheck(coll.gameObject);
            }
            else
            {
                isCollisionActiv = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (isCollisionActiv)
            {
                ColorCheck(coll.gameObject);
            }
            else
            {
                isCollisionActiv = true;
            }
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
            TryTeleport(gameObject);
        }
        else if (charType == "Purple")
        {
            ChangeGravity(gameObject);
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

    void TryTeleport(GameObject gameObject)
    {
        if (teleport != null)
        {
            GameObject teleportTo = null;
            if (teleport.GetComponent<ColorableBox>().typeOfTheBox == "Green")
            {
                teleportTo = teleport;
            }
            else if(teleport.GetComponent<ColorableBox>().teleport.GetComponent<ColorableBox>().typeOfTheBox == "Green")
            {
                teleportTo = teleport.GetComponent<ColorableBox>().teleport;
            }
            else if(teleport.GetComponent<ColorableBox>().teleport.GetComponent<ColorableBox>().teleport.GetComponent<ColorableBox>().typeOfTheBox == "Green")
            {
                teleportTo = teleport.GetComponent<ColorableBox>().teleport.GetComponent<ColorableBox>().teleport;
            } else
            {
                return;
            }
            teleportTo.GetComponent<ColorableBox>().GetPlayerAfterTeleporting();
            switch (actualSide)
            {
                case Sides.top:
                    gameObject.GetComponent<Transform>().position = new Vector2(
                        teleportTo.GetComponent<Transform>().position.x,
                        teleportTo.GetComponent<Transform>().position.y - teleportTo.GetComponent<Transform>().localScale.y * 1.1f);
                    break;
                case Sides.right:
                    gameObject.GetComponent<Transform>().position = new Vector2(
                        teleportTo.GetComponent<Transform>().position.x - teleportTo.GetComponent<Transform>().localScale.x,
                        teleportTo.GetComponent<Transform>().position.y);
                    break;
                case Sides.bottom:
                    gameObject.GetComponent<Transform>().position = new Vector2(
                        teleportTo.GetComponent<Transform>().position.x,
                        teleportTo.GetComponent<Transform>().position.y + teleportTo.GetComponent<Transform>().localScale.y * 1.1f);
                    break;
                case Sides.left:
                    gameObject.GetComponent<Transform>().position = new Vector2(
                        teleportTo.GetComponent<Transform>().position.x + teleportTo.GetComponent<Transform>().localScale.x * 1.1f,
                        teleportTo.GetComponent<Transform>().position.y);
                    break;
            }
        }
    }

    void GetPlayerAfterTeleporting()
    {
        isCollisionActiv = false;
    }
    void ChangeGravity(GameObject gameObject)
    {
        switch (actualSide)
        {
            case Sides.top:
                Physics2D.gravity = new Vector3(0, -9.82f, 0);
                gameObject.GetComponent<TestScript>().SetGravity(0);
                break;
            case Sides.right:
                Physics2D.gravity = new Vector3(-9.82f, 0, 0);
                gameObject.GetComponent<TestScript>().SetGravity(3);
                break;
            case Sides.bottom:
                Physics2D.gravity = new Vector3(0, 9.82F, 0);
                gameObject.GetComponent<TestScript>().SetGravity(2);
                break;
            case Sides.left:
                Physics2D.gravity = new Vector3(9.82F, 0, 0);
                gameObject.GetComponent<TestScript>().SetGravity(1);
                break;
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
            push = new Vector2(11, 0);
        }
        else if (actualSide == Sides.right)
        {
            push = new Vector2(-11, 0);
        }
        else
        {
            push = new Vector2(0, 11);
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
