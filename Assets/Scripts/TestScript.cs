using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour, IResetable
{

    private string typeOfCube = "Empty";
    private int gravity = 0;
    private bool isFacedRight = true;
    private float maxSpeed = 4f;
    private float jumpPower = 6.5f;
    public bool isGrounded = true;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private Rigidbody2D body;
    private new Transform transform;
    private Vector2 startPosition;
    private GameObject levelManager;

    private void Start()
    {
        transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        levelManager = FindObjectOfType<LevelManager>().gameObject;
    }

    public void SetGravity(int gravityDirection)
    {
        transform.Rotate(0, 0, 90 * gravityDirection - 90 * gravity);
        transform.Find("SideDetection").transform.Rotate(0, 0, 90 * -gravityDirection + 90 * gravity);
        gravity = gravityDirection;
    }

    public string GetTypeOfCube()
    {
        return typeOfCube;
    }


    public void SetTypeOfCube(string newType)
    {
        typeOfCube = newType;
    }

    public void Push(Vector2 direction)
    {
        body.AddForce(direction, ForceMode2D.Impulse);
    }

    public void Jump()
    {

        switch(gravity)
        {
            case 0:
                body.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                break;
            case 1:
                body.AddForce(new Vector2(-jumpPower, 0), ForceMode2D.Impulse);
                break;
            case 2:
                body.AddForce(new Vector2(0, -jumpPower), ForceMode2D.Impulse);
                break;
            case 3:
                body.AddForce(new Vector2(jumpPower, 0), ForceMode2D.Impulse);
                break;
        }
    }

    public void Move(float move)
    {
        switch (gravity)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
                body.velocity = new Vector3(maxSpeed * move, body.velocity.y);
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
                body.velocity = new Vector3(body.velocity.x, maxSpeed * move);
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
                body.velocity = new Vector3(-maxSpeed * move, body.velocity.y);
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
                body.velocity = new Vector3(body.velocity.x, -maxSpeed * move);
                break;
        }
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        float move = Input.GetAxis("Horizontal");
        if (move < 0)
        {
            isFacedRight = true;
        }
        else if(move > 0)
        {
            isFacedRight = false;
        }
        //gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
        //body.velocity = new Vector3(maxSpeed * move, body.velocity.y);
        Move(move);
    }



    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Danger")
        {
            levelManager.GetComponent<LevelManager>().ResetLevel();
        }
    }

    public void Reset()
    {
        body.velocity = new Vector2(0, 0);
        typeOfCube = "Empty";
        gameObject.GetComponent<SpriteRenderer>().sprite = levelManager.GetComponent<LevelManager>().GetSprite(typeOfCube + "Char");
        transform.position = startPosition;
    }

}
