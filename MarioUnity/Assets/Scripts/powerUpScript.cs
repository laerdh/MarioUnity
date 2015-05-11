using UnityEngine;
using System.Collections;

public class powerUpScript : MonoBehaviour {

	private int state;

	private Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setState(int state) {
		this.state = state;

		if (state >= 2) {
			animator.SetBool("isFlower", true);
		}
	}
}
