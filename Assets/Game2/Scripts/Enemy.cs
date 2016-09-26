using UnityEngine;
using System.Collections;
/// <summary>
/// Enemy on each spike
/// </summary>
public class Enemy : MonoBehaviour {

	public Vector2 speedRange;

	private float maxSpeed = 5;
	private Rigidbody2D rb;

	bool enemyFlag;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
	}


	void Start() {
	}

	void Update() {
		var newVelocity = rb.velocity;
		newVelocity.x = maxSpeed ;

		rb.velocity = newVelocity;

		invertVelocityInX ();
	}
	// invert the speed of the spike when crosses the bounds f the camera
	void invertVelocityInX() {
		if(transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x/2<findLeftCameraBound().x && !enemyFlag) {
			enemyFlag = true;
			maxSpeed = -maxSpeed;
		}
		if(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x/2>findRightCameraBound().x && !enemyFlag) {
			enemyFlag = true;
			maxSpeed = -maxSpeed;
		}
		if(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x/2<findRightCameraBound().x && transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x/2>findLeftCameraBound().x) {
			enemyFlag = false;
		}
	}

	Vector3 findLeftCameraBound() {
		return Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
	}

	Vector3 findRightCameraBound() {
		return Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 1));
	}

	// set random speed between the range assigned and position
	public void setPositionsAndSpeed() {
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;
		maxSpeed = Random.Range (speedRange.x, speedRange.y);
		if(Random.Range(1,10)%2 == 0) {
			maxSpeed = -maxSpeed;
		}
		float temp0 = Random.Range (transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x/2,transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x/2);
		transform.position = new Vector3 (temp0, transform.position.y, transform.position.z);
	}
	// if save me is clicked the disable this spike
	public void OnGameRevive() {
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
