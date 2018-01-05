using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    private string typeOfCube = "Empty";
    public float maxSpeed = 5f;
    private float jumpPower = 9f;
    private Color startColor = new Color(0f, 0f, 0f, 1f);
    private Color endColor = new Color(1f, 1f, 1f, 1f);
    private bool change = false;
    private float time = 0f;
    public bool isGrounded = true;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private List<GameObject> blocks;
    private BoxCollider2D box;
    private SpriteRenderer sprite;
    private Rigidbody2D body;
    private new Transform transform;
    public Transform start;
    private GameObject changingColor;


    private void Start()
    {
        transform = GetComponent<Transform>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
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
        body.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {

        //if (changingColor != null)
        //{
        //    if (change)
        //    {
        //        if (time <= 1)
        //        {
        //            time += Time.deltaTime / 5;
        //        }
        //    }
        //    else
        //    {
        //        if (time >= 0)
        //        {
        //            time -= Time.deltaTime / 5;
        //        }
        //        else
        //        {
        //            changingColor = null;
        //        }
        //    }
        //    changingColor.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, time);
        //    sprite.color = Color.Lerp(endColor, startColor, time);
        //}
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        float move = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Vertical");
        body.velocity = new Vector3(maxSpeed * move, body.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "SimpleBlock")
        {
            //changingColor = coll.gameObject;
            //change = true;
        }
        else if (coll.gameObject.tag == "Danger")
        {
            Respawn();
        }
    }

    void Respawn()
    {
        //changingColor = null;
        body.velocity = new Vector2(0, 0);
        transform.position = start.position;
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        //if (coll.gameObject.tag == "SimpleBlock")
        //{
        //    change = false;
        //}
    }
}
