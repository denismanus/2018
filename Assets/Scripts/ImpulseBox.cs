using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseBox : MonoBehaviour, IColorable
{

    private float impulsePover = 10f;

    private Color basicColor = new Color(1f, 0f, 0f, 1f);
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = basicColor;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (ColorComparison(collision.gameObject.GetComponent<SpriteRenderer>().color))
            {
                Push(collision.gameObject.GetComponent<Transform>());
            }
            else
            {
                ExchangeColor(collision.gameObject);
            }
        }
    }

    private void Push(Transform playerTransform)
    {
        //if(transform.position)
        //if ((transform.position.y - playerTransform.transform.position.y) < 0)
        //{
        //    Debug.Log("hit top");
        //}
        //if ((transform.position.x - playerTransform.transform.position.x) < 0)
        //{
        //    Debug.Log("hit left");
        //}
        //else if ((transform.position.x - playerTransform.transform.position.x) > 0)
        //{
        //    Debug.Log("hit right");
        //}
        //if (playerTransform.position.y < (float)(gameObject.GetComponent<Transform>().position.y + (float)gameObject.GetComponent<Transform>().transform.localScale.y / 2)) ;
        //{
        //    playerTransform.gameObject.GetComponent<TestScript>().Push(new Vector2(-impulsePover, impulsePover));
        //}
        //if (playerTransform.position.x < gameObject.GetComponent<Transform>().position.x && playerTransform.position.y > gameObject.GetComponent<Transform>().position.y)
        //{
        //    Debug.Log("2");
        //    playerTransform.gameObject.GetComponent<TestScript>().Push(new Vector2(0, impulsePover));
        //}
        //else if (playerTransform.position.x < gameObject.GetComponent<Transform>().position.x && playerTransform.position.y < gameObject.GetComponent<Transform>().position.y)
        //{
        //    Debug.Log("3");
        //    playerTransform.gameObject.GetComponent<TestScript>().Push(new Vector2(0, -impulsePover));
        //}
        //else if (playerTransform.position.x > gameObject.GetComponent<Transform>().position.x && playerTransform.position.y < gameObject.GetComponent<Transform>().position.y)
        //{
        //    Debug.Log("4");
        //    playerTransform.gameObject.GetComponent<TestScript>().Push(new Vector2(impulsePover, 0));
        //}
        //else if (playerTransform.position.x > gameObject.GetComponent<Transform>().position.x && playerTransform.position.y > gameObject.GetComponent<Transform>().position.y)
        //{
        //    Debug.Log("5");
        //    playerTransform.gameObject.GetComponent<TestScript>().Push(new Vector2(0, impulsePover)); ;
        //}
        //Debug.Log("6");
    }
    public bool ColorComparison(Color playerColor)
    {
        return gameObject.GetComponent<SpriteRenderer>().color.Equals(playerColor);
    }

    public void ExchangeColor(GameObject player)
    {
        Color foo = new Color(player.GetComponent<SpriteRenderer>().color.r,
            player.GetComponent<SpriteRenderer>().color.g,
            player.GetComponent<SpriteRenderer>().color.b,
            1f);
        player.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = foo;
    }

}
