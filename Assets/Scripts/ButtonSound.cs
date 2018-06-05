using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour {

    public delegate void MethodContainer(string message);
    public static event MethodContainer OnAction;

    // Use this for initialization
    void Start () {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => SendSound());
	}

	private void SendSound()
    {
        OnAction("Click");   
    }
	
}
