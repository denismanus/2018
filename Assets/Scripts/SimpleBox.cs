using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBox : MonoBehaviour {

    private bool isChanging;

    private Color basicColor;

    private GameObject character;

	void Start () {
		
	}
	

	void Update () {
        if (isChanging)
        {
            changeColor();
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            character = collision.gameObject;
            isChanging = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isChanging = false;
        }
    }

    void changeColor()
    {

    }

    void undo()
    {

    }
}
