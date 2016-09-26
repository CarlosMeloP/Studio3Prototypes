using UnityEngine;
using System.Collections;
/// <summary>
/// Player script controls the behaviour of the player
/// </summary>
public class Player : MonoBehaviour {
	public float jumpSpeed = 20;
	public float maxSpeed = 5;
	private Rigidbody2D rb;

	private bool canAddScore;
	// events to handle game over action
	public delegate void GameOverAction();
	public static event GameOverAction OnGameOver;
	// events to handle score actions
	public delegate void OnIncreaseScoreAction();
	public static event OnIncreaseScoreAction OnIncreaseScore;

	public static float distanceTraveled;
	bool PlayerFlag;
	bool doubleJump;

	GameObject killerByEnemy;
	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start () {		
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, Mathf.Abs(maxSpeed));
		GameController.OnGameStart += OnGameStart;
		GameController.OnGameRevive += OnGameRevive;
	}
	
	// Update is called once per frame
	void Update () {
		distanceTraveled = transform.position.y;
		if(GameState.Instance.gameState == GameState.GAMESTATE.GAMERESUME) {			
			var newVelocity = rb.velocity;
			if (Input.GetMouseButtonDown(0) && !doubleJump) {
				newVelocity.y = jumpSpeed ;
				if (Mathf.Abs (rb.velocity.y) > 0) {
					doubleJump = true;
				}
			}

			#if UNITY_ANDROID || UNITY_IOS
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began && !doubleJump) {
					newVelocity.y = jumpSpeed ;
					if (Mathf.Abs (rb.velocity.y) > 0) {
						doubleJump = true;
					}
				}
			}
			#endif

			newVelocity.x = maxSpeed ;

			rb.velocity = newVelocity;
			invertVelocityInX ();
		}
	}

	void OnDestroy() {
		GameController.OnGameStart -= OnGameStart;
		GameController.OnGameRevive -= OnGameRevive;
	}

	void FixedUpdate() {
	}

	void OnGameStart() {
		canMove (true);
	}

	void gameOver() {
		GameState.Instance.gameState = GameState.GAMESTATE.GAMEOVER;
		canMove (false);
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().isTrigger = true;
		if(OnGameOver!=null) {
			OnGameOver ();
		}
	}

	public void OnGameRevive() {
		killerByEnemy.GetComponent<Enemy> ().OnGameRevive ();	
		canMove (true);
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().isTrigger = false;
	}
	// invert the speed of the player when crosses the bounds f the camera
	void invertVelocityInX() {
		if(transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x/2<findLeftCameraBound().x && !PlayerFlag) {
			PlayerFlag = true;
			maxSpeed = -maxSpeed;
		}
		if(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x/2>findRightCameraBound().x && !PlayerFlag) {
			PlayerFlag = true;
			maxSpeed = -maxSpeed;
		}
		if(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x/2<findRightCameraBound().x && transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x/2>findLeftCameraBound().x) {
			PlayerFlag = false;
		}
	}

	Vector3 findLeftCameraBound() {
		return Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
	}

	Vector3 findRightCameraBound() {
		return Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 1));
	}
	void OnCollisionEnter2D(Collision2D arg0) {
		if(arg0.transform.tag == "platform" && !arg0.transform.GetComponentInParent<CreateWalls> ().isTrigger) {
			arg0.transform.GetComponentInParent<CreateWalls> ().isTrigger = true;
			if(OnIncreaseScore!=null) {
				OnIncreaseScore ();
			}
		}
		if(arg0.transform.tag == "enemy" && GameState.Instance.gameState == GameState.GAMESTATE.GAMERESUME) {
			killerByEnemy = arg0.transform.gameObject;
			gameOver ();
		}
		doubleJump = false;
	}

	void OnCollisionExit2D(Collision2D arg0) {
	}

	void OnTriggerExit2D(Collider2D arg0) {
	}

	public void canMove(bool arg0) {
		if (arg0) { 
			rb.isKinematic = false;
		} else {
			rb.isKinematic = true;
		}
	}
}
