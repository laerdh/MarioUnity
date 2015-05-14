using UnityEngine;
using System.Collections;

public class cameraWall : MonoBehaviour {

	public Transform wallPosition;

	Vector3 leftEdgeOfScreen;

	// Use this for initialization
	void Start () {
		// Get The edge of the screen
		leftEdgeOfScreen = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0));

	}
	
	// Update is called once per frame
	void Update () {
		// Get The edge of the screen
		leftEdgeOfScreen = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0));

		wallPosition.transform.position = leftEdgeOfScreen;
	}
}
