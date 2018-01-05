
using UnityEngine;

public class ColorableBox : MonoBehaviour
{

    public string typeOfTheBox;
    public Transform positionOfTeleport;
    private GameObject levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>().gameObject;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            ColorCheck(coll.gameObject);
        }
    }

    //void OnCollisionExit2D(Collision2D coll)
    //{
    //    if (coll.gameObject.tag == "Player")
    //    {
    //        ColorCheck(coll.gameObject);
    //    }
    //}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            ColorCheck(coll.gameObject);
        }
    }
    //void OnTriggerExit2D(Collider2D coll)
    //{
    //    if (coll.gameObject.tag == "Player")
    //    {
    //        ColorCheck(coll.gameObject);
    //    }
    //}
    void ColorCheck(GameObject gameObject)
    {
       
        GetComponent<BoxCollider2D>().isTrigger = false;
        string charType = gameObject.GetComponent<TestScript>().GetTypeOfCube();
        Debug.Log(charType + " " + typeOfTheBox);
        if (charType != typeOfTheBox)
        {
            ExchangeColor(gameObject, charType);
        }
        else if (charType == "Blue"&&typeOfTheBox=="Blue")
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if(charType=="Red")
        {
           Push(gameObject);
        }
        else if(charType=="Green")
        {
            TeleportTo(gameObject);
        }
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
        if(positionOfTeleport!=null)
        {
            gameObject.GetComponent<Transform>().position = new Vector2(positionOfTeleport.position.x, positionOfTeleport.position.y + positionOfTeleport.localScale.y);
        }
    }

    void Push(GameObject gameObject)
    {

    }
}
