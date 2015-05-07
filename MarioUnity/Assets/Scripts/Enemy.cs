using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Rigidbody2D enemyTurtle;
	private float speed = -1f;
	// Use this for initialization
	void Start () 
	{
		enemyTurtle = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		enemyTurtle.transform.localScale = new Vector3 (5, 5, 0);
		enemyTurtle.transform.Translate (new Vector2(speed, 0) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "pipe") {
			Debug.Log ("Test");
		}
	}
}
