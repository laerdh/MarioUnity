using UnityEngine;
using System.Collections;

public class EnemyKoopa : MonoBehaviour {
	public PlayerMoveScript mario;
	private Animator anim;
	public Rigidbody2D enemy;
	public float moveSpeed = -1f;
	public Transform sightStart;
	public Transform sightEnd;
	public LayerMask detectObject;
	public Transform weakness;

	private bool colliding;

	private GameObject thePlayer;
	private bool isAwake;
	private bool started = false;

	private int lives = 2;

	public LayerMask theGround;
	public LayerMask thePlayerLayer;

	// Use this for initialization
	void Start () 
	{
		enemy = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		thePlayer = GameObject.Find ("Player");
		mario = thePlayer.GetComponent<PlayerMoveScript> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		float distance = Vector2.Distance (thePlayer.transform.position, transform.position);
		if (distance < 13)
			isAwake = true;

		if (isAwake) {
			if(!started) {
				enemy.velocity = new Vector2 (moveSpeed, enemy.velocity.y);
				started = true;
			}
			enemy.velocity = new Vector2 (moveSpeed, enemy.velocity.y);
		
			colliding = Physics2D.Linecast (sightStart.position, sightEnd.position, detectObject);
		
			if (colliding) {
				transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
				moveSpeed *= -1;
			}

			if(lives == 1) {
				RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2(moveSpeed/3,0f), 1f, theGround);
				Debug.DrawRay (transform.position, new Vector2(moveSpeed/3,0), Color.red);
			
				if (hit) {
					print (hit.collider.tag);
					if(hit.collider.tag == "Untagged") {
						moveSpeed = -moveSpeed;
					}
				}

				RaycastHit2D hit2 = Physics2D.Raycast (transform.position, new Vector2(moveSpeed/3,0f), 1f, thePlayerLayer);
				Debug.DrawRay (transform.position, new Vector2(moveSpeed/3,0), Color.green);

				if (hit2) {
					print (hit2.collider.tag);
					if(hit2.collider.tag == "Player") {
						moveSpeed = -moveSpeed;
						mario.removeLife();
					}
				}
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
		if (other.gameObject.tag == "Player") {
			GameObject.Find ("AudioController").GetComponent<AudioManager> ().StompPlay();
			bool isSuper = mario.getHasSuperStar ();
			if (isSuper) {
				Dies ();
			} else if (lives == 2) {
				if (!isSuper) {
					float height = other.contacts [0].point.y - weakness.position.y;
			
					if (height > 0) {
						//Dies();
						lives--;
						if (lives == 0)
							Dies ();

						if (lives == 1) {
							enemy.velocity = new Vector2 (0, 0);
							anim.SetBool ("isHit", true);
						}

						other.rigidbody.AddForce (new Vector2 (0, 300));
					} else {
						mario.removeLife ();
					}
				}
			} else if (lives == 1) {

				float distance = transform.position.x - other.transform.position.x;
				if (distance >= 0)
					moveSpeed = 7;//enemy.velocity = new Vector2(7,enemy.velocity.y);
				else if (distance < 0)
					moveSpeed = -7;//enemy.velocity = new Vector2(-7,enemy.velocity.y);
				other.rigidbody.AddForce (new Vector2 (0, 300));
			}

		} else if (other.gameObject.tag == "deadly" && lives == 1) {
			if (moveSpeed > 4 || moveSpeed < -4) {
				print ("Jeg blir kørt");
				GameObject.Find ("AudioController").GetComponent<AudioManager> ().KickPlay();
				EnemyKill k = other.gameObject.GetComponent<EnemyKill> ();
				if (k != null)
					k.gotShot ();
			}
		} else if(other.gameObject.tag == "pipe" ) {
			moveSpeed = moveSpeed * -1;
		}
	}
	
	void Dies() 
	{
		moveSpeed = 0;
		anim.SetBool ("isHit", true);
		Destroy (this.gameObject, 0.5f);
		gameObject.tag = "neutralized";
	}
}
