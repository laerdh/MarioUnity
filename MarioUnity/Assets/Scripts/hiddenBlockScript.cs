using UnityEngine;
using System.Collections;

public class hiddenBlockScript : MonoBehaviour {
	
	public Transform point;

	public PlayerMoveScript player;

	private BoxCollider2D[] boxes;

	// Use this for initialization
	void Start () {
		player = player.GetComponent<PlayerMoveScript> ();
		boxes = GetComponents<BoxCollider2D> ();
		foreach(BoxCollider2D b in boxes) {
			b.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (GetComponent<BoxCollider2D> ().enabled);
		Vector2 playerPos = player.getPos ();
		if (playerPos.x > point.position.x && player.isGrounded ()) {
			foreach(BoxCollider2D b in boxes) {
				b.enabled = true;;
			}
		}
	}
}
