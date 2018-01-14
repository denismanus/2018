using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitBlock : MonoBehaviour {

    private PauseMenu pause;
    private Animator anim;

	void Start () {
        pause = FindObjectOfType<PauseMenu>();
        anim = GetComponent<Animator>();
    }


    public void CallNextLevel()
    {
        pause.NextLevelMenu();
    }
	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            anim.SetBool("isComingOut", true);   
        }
    }
}
