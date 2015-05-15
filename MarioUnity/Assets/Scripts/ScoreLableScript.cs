using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLableScript : MonoBehaviour {

	private TextMesh text;

	private int ttl = 100;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		ttl--;
		transform.position = new Vector3 (transform.position.x, transform.position.y + 0.05f, -8);
		if (ttl < 0)
			Destroy (this.gameObject);
	}

	public void setScore(int score) {
		GetComponent<TextMesh> ().text = "" + score;
	}

	public void setScore(string score) {
		GetComponent<TextMesh> ().text = score;
	}
}
