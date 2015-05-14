using UnityEngine;
using System.Collections;

public class flagPole : MonoBehaviour {
	public GameObject scoreLable;
	public Score score;
	public PlayerMoveScript player;
	private ScoreLableScript sc;



	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter2D(Collision2D other){
		Debug.Log ("flag");
		if (other.gameObject.tag == "Player") {
			if (player.transform.position.y > 2 && player.transform.position.y < 3) {
				score.AddScoreFlag (100);
				GameObject e = GameObject.Instantiate (scoreLable);
				e.transform.position = new Vector3 (transform.position.x - 0.5f, transform.position.y + 1.5f, -8f);
				sc = e.GetComponent<ScoreLableScript> ();
				if (sc != null) {
					sc.setScore (100);
				}
			}  
			if (player.transform.position.y > 3 && player.transform.position.y < 5) {
					
				score.AddScoreFlag (400);
				GameObject e = GameObject.Instantiate (scoreLable);
				e.transform.position = new Vector3 (transform.position.x - 0.5f, transform.position.y + 1.5f, -8f);
				sc = e.GetComponent<ScoreLableScript> ();
				if (sc != null) {
					sc.setScore (400);
				}

			}
			if (player.transform.position.y > 5 && player.transform.position.y<7) {
				score.AddScoreFlag (800);
				GameObject e = GameObject.Instantiate (scoreLable);
				e.transform.position = new Vector3 (transform.position.x - 0.5f, transform.position.y + 1.5f, -8f);
				sc = e.GetComponent<ScoreLableScript> ();
				if (sc != null) {
					sc.setScore (800);
				}

			}
			if (player.transform.position.y > 7 && player.transform.position.y<11) {
				score.AddScoreFlag (2000);
				GameObject e = GameObject.Instantiate (scoreLable);
				e.transform.position = new Vector3 (transform.position.x - 0.5f, transform.position.y + 1.5f, -8f);
				sc = e.GetComponent<ScoreLableScript> ();
				if (sc != null) {
					sc.setScore (2000);
				}
				
			}

			if (player.transform.position.y > 11) {
				score.AddScoreFlag (4000);
				GameObject e = GameObject.Instantiate (scoreLable);
				e.transform.position = new Vector3 (transform.position.x - 0.5f, transform.position.y + 1.5f, -8f);
				sc = e.GetComponent<ScoreLableScript> ();
				if (sc != null) {
					sc.setScore (4000);
				}
				
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
