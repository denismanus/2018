using System.Collections;
using UnityEngine;

public class ColorableBox : MonoBehaviour
{

    private enum Sides
    {
        top, right, bottom, left
    }
    private static bool isInAction = false;
    private bool isInCollision = false;
    private Animator animator;
    private Vector2 positionToTeleport;
    private bool isCollisionActiv = true;
    public Block teleportCoord;
    private Sides actualSide;
    private Sides gravitySide;
    public string typeOfTheBox;
    private string basicColor;
    public GameObject teleport;
    private LevelManager levelManager;
    private GameObject character;
    public bool isCharacterInside = false;
    void Start()
    {

        animator = GetComponent<Animator>();
        basicColor = typeOfTheBox;
        levelManager = FindObjectOfType<LevelManager>();
        SetTypeOfCube(basicColor);
    }


    public void SetTypeOfCube(string newType)
    {
        typeOfTheBox = newType;
        animator.SetBool("Red", false);
        animator.SetBool("Green", false);
        animator.SetBool("Blue", false);
        animator.SetBool("Purple", false);
        animator.SetBool("Empty", false);
        animator.SetBool(newType, true);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!isInCollision)
        {
            isInCollision = true;
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
        
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        isInCollision = false;
        if (coll.gameObject.tag == "Player")
        {
            animator.SetBool("isFailTeleporting", false);
            animator.SetBool("isTeleporting", false);
            isCharacterInside = false;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isCharacterInside = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isCharacterInside = true;
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
        else if(coll.gameObject.tag == "GravityDetect")
        {
            GetGravityCollision(coll);
        }
    }
    void ColorCheck(GameObject gameObject)
    {

        string charType = gameObject.GetComponent<TestScript>().GetTypeOfCube();
        if (charType != typeOfTheBox)
        {
            if(typeOfTheBox=="Blue")
            {
                levelManager.ChangeBlueTrigger(true);
                SetTriggers(false);
            }
            else
            {
                levelManager.ChangeBlueTrigger(false);
            }
            ExchangeColor(gameObject, charType);
        }
        else if (charType == "Blue")
        {
           
            //SetTriggers(true);
            //this.gameObject.layer = 0;
            //transform.parent.gameObject.layer = 0;
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
        SetTriggers(false);
        SetTypeOfCube(basicColor);
    }


    void ExchangeColor(GameObject gameObject, string charType)
    {

        gameObject.GetComponent<TestScript>().SetTypeOfCube(typeOfTheBox);
        typeOfTheBox = charType;
        SetTypeOfCube(typeOfTheBox);

    }
    GameObject FindTeleport(GameObject teleport)
    {
        if (teleport.GetComponentInChildren<ColorableBox>().typeOfTheBox != "Green")
        {
            return FindTeleport(teleport.GetComponentInChildren<ColorableBox>().teleport);
        }
        else
        {
            return teleport;
        }

    }
    bool CheckIfTeleportInWall(Vector2 checkPosition)
    {
        return levelManager.FindBlockInPosition(checkPosition);
    }

    void TryTeleport(GameObject gameObject)
    {

        GameObject teleportTo = FindTeleport(teleport);
        if (teleportTo != null)
        {
            positionToTeleport = new Vector2();
            Vector2 positionToCheck = new Vector2();
            switch (gravitySide)
            {
                case Sides.top:
                    positionToTeleport = teleportTo.GetComponent<Transform>().position;
                    positionToCheck = teleportTo.GetComponent<Transform>().position;
                    positionToTeleport.y -= teleportTo.GetComponent<Transform>().localScale.y * 1.1f;
                    positionToCheck.y -= teleportTo.GetComponent<Transform>().localScale.y;
                    break;
                case Sides.right:
                    positionToTeleport = teleportTo.GetComponent<Transform>().position;
                    
                    positionToTeleport.x -= teleportTo.GetComponent<Transform>().localScale.x;
                    positionToCheck = positionToTeleport;

                    break;
                case Sides.bottom:
                    positionToTeleport = teleportTo.GetComponent<Transform>().position;
                    positionToCheck = teleportTo.GetComponent<Transform>().position;
                    positionToTeleport.y += teleportTo.GetComponent<Transform>().localScale.y * 1.1f;
                    positionToCheck.y += teleportTo.GetComponent<Transform>().localScale.y;

                    break;
                case Sides.left:
                    positionToTeleport = teleportTo.GetComponent<Transform>().position;
                    positionToCheck = teleportTo.GetComponent<Transform>().position;
                    positionToTeleport.x += teleportTo.GetComponent<Transform>().localScale.x * 1.1f;
                    positionToCheck.x += teleportTo.GetComponent<Transform>().localScale.x;
                    break;
            }
            if (CheckIfTeleportInWall(positionToCheck))
            {
               
                character = gameObject;
                character.GetComponent<TestScript>().isConrtolBlocked = true;
                gameObject.GetComponent<TestScript>().SetStun(true);
                animator.SetBool("isTeleporting", true);
                character.GetComponent<TestScript>().Teleport(true);
                teleportTo.GetComponentInChildren<ColorableBox>().GetPlayerAfterTeleporting();
                StartCoroutine(Teleporting());
            }
            else
            {
                animator.SetBool("isFailTeleporting", true);
            }
        }
        else
        {
            animator.SetBool("isFailTeleporting", true);
        }
    }

    public void SetTriggers(bool value)
    {

        if (!isCharacterInside)
        {
            transform.parent.GetComponent<BoxCollider2D>().isTrigger = value;
            foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
            {
                box.isTrigger = value;
            }
            if (value)
            {
                gameObject.layer = 0;
                transform.parent.gameObject.layer = 0;
            }
            else
            {
                gameObject.layer = 8;
                transform.parent.gameObject.layer = 8;
            }
        }
        else
        {
            StartCoroutine(Wait(value));
        }
    }
    IEnumerator Teleporting()
    {
        yield return new WaitForSeconds(0.7f);
        EndTeleporting();
    }
    IEnumerator Wait(bool value)
    {
        yield return new WaitForSeconds(0.6f);
        SetTriggers(value);
    }

    IEnumerator StartAction()
    {
        isInAction = true;
        yield return new WaitForSeconds(0.1f);
        isInAction = false;
    }


    public void EndTeleporting()
    {
       
        character.GetComponent<TestScript>().Teleport(false);
        animator.SetBool("isTeleporting", false);
        character.GetComponent<Transform>().position = positionToTeleport;
        character.GetComponent<TestScript>().SetStun(false);
        character.GetComponent<TestScript>().isConrtolBlocked = false;
    }
    
    public void EndFailTeleporting()
    {
        animator.SetBool("isFailTeleporting", false);
    }
    void GetPlayerAfterTeleporting()
    {
        isCollisionActiv = false;
    }
    void ChangeGravity(GameObject gameObject)
    {
        switch (gravitySide)
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
        Vector2 player = gameObject.GetComponent<Transform>().position;
        Vector2 block = this.GetComponent<Transform>().position;
        Vector2 push = new Vector2();
        int gravity = gameObject.GetComponent<TestScript>().GetGravity();
      
        switch (gravity)
        {
            case 0:
                push = new Vector2(0, 11);
                break;
            case 1:
                push = new Vector2(-11, 0);
                break;
            case 2:
                push = new Vector2(0, -11);
                break;
            case 3:
                push = new Vector2(11, 0);
                break;
        }

        if(!isInAction)
        {
            Debug.Log(actualSide);
            if (actualSide == Sides.bottom)
            {
                StartCoroutine(StartAction());
                gameObject.GetComponent<TestScript>().Push(push);
            }
        }
        
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
    private void GetGravityCollision(Collider2D gameObject)
    {
        switch (gameObject.name)
        {
            case "Left":
                gravitySide = Sides.left;
                break;
            case "Bottom":
                gravitySide = Sides.bottom;
                break;
            case "Right":
                gravitySide = Sides.right;
                break;
            case "Top":
                gravitySide = Sides.top;
                break;
        }
    }

    
}
