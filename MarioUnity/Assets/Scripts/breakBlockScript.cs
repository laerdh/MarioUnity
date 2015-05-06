using UnityEngine;
using System.Collections;

public class breakBlockScript : MonoBehaviour {

	/*
	 * This script is to make sure that the block is destroyed if th player hits it from the bottom
	 */

	private bool isHit;
	private Vector2 pos;
	
	public GameObject content;
	private int boxtype;
	private const int NORMALBLOCK = 0;
	private const int QUESTIONBLOCK = 1;
	private const int HARDBLOCK = 2;
	private const int HIDDENBLOCK = 3;
	private const int MULTICOIN = 4;

	public GameObject breakBlockPrefab;

	public enum BoxTypes {
		NORMALBLOCK, QUIESTIONBLOCK, HARDBLOCK, HIDDENBLOCK
	}
	public BoxTypes boxTypes;

	void Start() {
		boxtype = (int)boxTypes;
		isHit = false;
		pos = new Vector2 (transform.position.x, transform.position.y -0.2f);
	}

	void Update() {
		if(isHit) {
			print ("hit");
			SpawnExplosion();
			Destroy(this.gameObject);

		}
	}

	void SpawnExplosion() {
		GameObject e1 = GameObject.Instantiate (breakBlockPrefab);
		e1.transform.position = transform.position;
	}

	public void setHit(bool isHit) {
		this.isHit = isHit;
	}
}
