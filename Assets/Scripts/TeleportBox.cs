using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBox : MonoBehaviour, IColorable {

    public GameObject teleportTo;

    private Color basicColor = new Color(0f, 1f, 0f, 1f);
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().color = basicColor;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(ColorComparison(collision.gameObject.GetComponent<SpriteRenderer>().color))
            {
                if(teleportTo!=null)
                {
                    teleport(collision.gameObject);
                }
            }
            else
            {
                ExchangeColor(collision.gameObject);
            }
        }
    }
    
    private void teleport(GameObject player)
    {
        player.transform.position = new Vector3(teleportTo.transform.position.x, 
            teleportTo.transform.position.y + teleportTo.transform.localScale.y,
            teleportTo.transform.position.z);
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
