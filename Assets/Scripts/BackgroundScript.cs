using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public Transform cameraPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
        GetComponent<Transform>().position = new Vector3(cameraPosition.position.x, cameraPosition.position.y, 2);
	}
}
