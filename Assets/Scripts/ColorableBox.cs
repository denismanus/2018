﻿
using UnityEngine;

public class ColorableBox : MonoBehaviour
{

    private enum Sides
    {
        top, right, bottom, left
    }
    private Animator animator;
    private Vector2 positionToTeleport;
    private bool isCollisionActiv = true;
    public Block teleportCoord;
    private Sides actualSide;
    public string typeOfTheBox;
    private string basicColor;
    public GameObject teleport;
    private LevelManager levelManager;
    private GameObject character;
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

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            animator.SetBool("isFailTeleporting", false);
            animator.SetBool("isTeleporting", false);
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
        this.gameObject.layer = 8;
        string charType = gameObject.GetComponent<TestScript>().GetTypeOfCube();
        if (charType != typeOfTheBox)
        {
            ExchangeColor(gameObject, charType);
        }
        else if (charType == "Blue" && typeOfTheBox == "Blue")
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            this.gameObject.layer = 0;
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
        if (teleport.GetComponent<ColorableBox>().typeOfTheBox != "Green")
        {
            return FindTeleport(teleport.GetComponent<ColorableBox>().teleport);
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
            switch (actualSide)
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
                animator.SetBool("isTeleporting", true);
                teleportTo.GetComponent<ColorableBox>().GetPlayerAfterTeleporting();
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

    public void EndTeleporting()
    {
        animator.SetBool("isTeleporting", false);
        character.GetComponent<Transform>().position = positionToTeleport;
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
        Debug.Log(actualSide);
    }
}
