using UnityEngine;
using System.Collections;

public class ExtraLifeScript: MonoBehaviour {
	
	private Rigidbody2D obj;
	
	private int moveSpeed = 3;

	public GameObject scoreLable;
	private ScoreLableScript sc;
	public Score score;

	
	// Use this for initialization
	void Awake() {
		obj = GetComponent<Rigidbody2D> ();
		GameObject.Find ("AudioController").GetComponent<AudioManager> ().pwrUpSpawn();
		score = GameObject.Find ("Score").GetComponent<Score> ();
	}
	
	// Update is called once per frame
	void Update () {
		obj.velocity = new Vector2 (moveSpeed, obj.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {

			GameObject e = GameObject.Instantiate (scoreLable);
			e.transform.position = new Vector3(transform.position.x - 0.5f,transform.position.y + 1.5f, -8f);
			sc = e.GetComponent<ScoreLableScript> ();
			if (sc != null) {
				sc.setScore("1UP");
			}

			GameObject.Find ("AudioController").GetComponent<AudioManager> ().PlayExtra();
			Destroy(this.gameObject);
		}
	}
} 
