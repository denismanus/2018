using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{

    enum Action
    {
        Jumping,
        Moving
    }
    private Action state;
    private float jumpPower = 2f;
    private float speed = 2f;
    private bool isGrounded = true;

    private new Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private new Transform transform;

    public Actor(GameObject gameObject)
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        transform = gameObject.GetComponent<Transform>();
    }

    public void MoveLeft()
    {
        rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
    }

    public void MoveRight()
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
    }

    public void Respawn()
    {
        Debug.Log("Respawn");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("TemporaryColorBlock"))
        {

        }
    }
}
