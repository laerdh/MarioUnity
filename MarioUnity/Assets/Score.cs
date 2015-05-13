using UnityEngine;
using System.Collections;
<<<<<<< HEAD

public class Score : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
=======
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public Text text;
	public int currentScore;
	public int score = 100;

	// Use this for initialization
	void Start () {

>>>>>>> 6fe83932a5cd48f3bfa6b8c902fca7f04c95531f
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
	
=======
		text.text = currentScore.ToString ("000000");
	}

	public void AddScore () 
	{
		currentScore += score;
>>>>>>> 6fe83932a5cd48f3bfa6b8c902fca7f04c95531f
	}
}
