using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBlock : MonoBehaviour {

    // Use this for initialization
    private LevelManager manager;
	void Start () {
        manager = FindObjectOfType<LevelManager>();
	}
	
	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            manager.ResetLevel();
        }
    }
}
