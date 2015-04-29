using UnityEngine;
using System.Collections;

public class cameraWall : MonoBehaviour {

	public Transform wallPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		wallPosition.transform.position = new Vector3(transform.position.x - 8,0f,0f);
	}
}
