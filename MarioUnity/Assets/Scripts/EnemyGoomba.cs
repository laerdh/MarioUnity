using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyGoomba : MonoBehaviour {
	private PlayerMoveScript mario;
	public Score score;

	public GameObject scoreLable;
	private ScoreLableScript sc;

	private Animator anim;
	public Rigidbody2D enemy;
	public float velocity = -1f;
	public Transform sightStart;
	public Transform sightEnd;
	public LayerMask detectObject;
	public Transform weakness;

	public Text text;

	private bool colliding;

	private GameObject thePlayer;
	private bool isAwake;


	// Use this for initialization
	void Start () 
	{
		enemy = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		thePlayer = GameObject.Find ("Player");
		score = GameObject.Find ("Score").GetComponent<Score> ();
		mario = thePlayer.GetComponent<PlayerMoveScript> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		float distance = Vector2.Distance (thePlayer.transform.position, transform.position);
		if (distance < 10)
			isAwake = true;

		if (isAwake) {
			enemy.velocity = new Vector2 (velocity, enemy.velocity.y);

			colliding = Physics2D.Linecast (sightStart.position, sightEnd.position, detectObject);

			if (colliding) {
				transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
				velocity *= -1;
			}
		}
	}

	void OnDrawGizmos() 
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (sightStart.position, sightEnd.position);
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			float height = other.contacts[0].point.y - weakness.position.y;

			if (height > 0) 
			{
				if(this.gameObject.tag == "deadly"){
				Dies();
				score.AddScore();
				other.rigidbody.AddForce(new Vector2 (0, 300));
				}
			} else 
			{
				mario.Dies ();
			}
		}
	}

	void Dies() 
	{
		GetComponent<Collider2D>().enabled = false;

		velocity = 0;
		anim.SetBool ("isHit", true);
		score.AddScore ();

		GameObject e = GameObject.Instantiate (scoreLable);
		e.transform.position = new Vector3(transform.position.x - 0.5f,transform.position.y + 1.5f, -8f);
		sc = e.GetComponent<ScoreLableScript> ();
		if (sc != null) {
			sc.setScore(100);
		}

		//text.text = "100";
		gameObject.tag = "neutralized";	
		Destroy (this.gameObject, 0.5f);
	}

}
