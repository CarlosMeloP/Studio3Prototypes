using UnityEngine;
using System.Collections;
/// <summary>
/// Create platform and set spike speed and position
/// </summary>
public class CreateWalls : MonoBehaviour {

	public bool isTrigger;

	Rigidbody2D playerRb;

	void Awake() {
		playerRb = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D>();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (playerRb.velocity.y > 0) {
			jumpGoingUp ();
		} else if (playerRb.velocity.y < 0) {
			jumpGoingDown ();
		}
	}

	public void positionEnemies() {
		isTrigger = false;
		GetComponentInChildren<Enemy> ().setPositionsAndSpeed ();
	}
	// if the ball is going up disable the box collider2d on the platform
	void jumpGoingUp() {
		if(GetComponentInChildren<BoxCollider2D>()!=null) {
			GetComponentInChildren<BoxCollider2D> ().enabled = false;
		}
	}
	// if the ball is going down enable the box collider2d on the platform
	void jumpGoingDown() {
		if(GetComponentInChildren<BoxCollider2D>()!=null) {
			GetComponentInChildren<BoxCollider2D> ().enabled = true;
		}
	}
}
