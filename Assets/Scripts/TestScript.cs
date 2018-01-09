using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour, IResetable
{

    private string typeOfCube = "Empty";
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
        //body.velocity = direction;  
    }

    public void Jump()
    {
        body.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
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

        gameObject.GetComponent<SpriteRenderer>().flipX = isFacedRight;
       body.velocity = new Vector3(maxSpeed * move, body.velocity.y);
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Danger")
        {
           Reset();
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
