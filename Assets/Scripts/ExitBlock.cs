using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitBlock : MonoBehaviour {

    private GameObject menu;
    

	void Start () {
        menu = GameObject.FindGameObjectWithTag("NextLevelMenu");
        menu.gameObject.SetActive(false);
    }


	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            menu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
