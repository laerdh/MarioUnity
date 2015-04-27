using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

	// OverWorld Camera Y position
	private const float OVERWORLD_Y = 6.66f;

	// Target to follow
	public GameObject target;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (target.GetComponent<Transform> ().position.x + 8,OVERWORLD_Y, -5);
	}
}
