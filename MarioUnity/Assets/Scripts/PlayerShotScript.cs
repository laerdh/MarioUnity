using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShotScript : MonoBehaviour {

	// To get reference of playerLives
	public PlayerMoveScript player;

	// Reference to the object that will be shot
	public GameObject fireBall;

	// A list of the objects. only shoot if the list is less than 3
	private List<GameObject> fireList;



	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerMoveScript> ();
		fireList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		print ("fitttrtteeee");
		if (Input.GetKeyDown (KeyCode.LeftControl) && player.getLives() > 2 && fireList.Count < 2) {
			GameObject ball = GameObject.Instantiate(fireBall);
			if(player.getDir()==1)
				ball.transform.position = new Vector2(player.transform.position.x + 0.5f,player.transform.position.y);
			else if(player.getDir()==-1)
				ball.transform.position = new Vector2(player.transform.position.x + -0.5f,player.transform.position.y);

			ball.GetComponent<FireBallScript>().setDirection(player.getDir());

			fireList.Add(ball);
		}

	}

	// Remove from List
	public void removeFromList(GameObject o) {
		fireList.Remove (o);
	}
}
