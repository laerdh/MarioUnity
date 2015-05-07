using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private float speed = -1f;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{	
		GetComponent<Rigidbody2D>().transform.localScale = new Vector3 (5, 5, 0);
		transform.Translate (new Vector2(speed, 0) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		print (other.gameObject.name + " has entered the trigger");
	}
}
