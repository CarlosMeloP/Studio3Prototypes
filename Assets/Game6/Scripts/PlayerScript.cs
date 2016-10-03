using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float jumpPower = 10.0f;
    Rigidbody2D myRigidbody;
    bool isGrounded = false;
    float posX = 0.0f;
    bool isGameOver = false;
    ChallengeController myChallengeController;
    GameControl myGameController;

    // Use this for initialization
    void Start () {
        myRigidbody = transform.GetComponent<Rigidbody2D>();
        posX = transform.position.x;
        myChallengeController = GameObject.FindObjectOfType<ChallengeController>();
        myGameController = GameObject.FindObjectOfType<GameControl>();

    }
	
	
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.Space) && isGrounded && !isGameOver) {
            
            myRigidbody.AddForce(Vector3.up * (jumpPower * myRigidbody.mass * myRigidbody.gravityScale * 20.0f));
            isGrounded = false;
        }
        if (Input.touchCount>0 && isGrounded && !isGameOver)
        {

            myRigidbody.AddForce(Vector3.up * (jumpPower * myRigidbody.mass * myRigidbody.gravityScale * 20.0f));
            isGrounded = false;
        }

        //Hit in face check
        if (transform.position.x < posX && !isGameOver) {
            GameOver();
        }


	}

    void GameOver() {
        isGameOver = true;
        myChallengeController.GameOver();
        GameControl.Restart();
    }



    void OnCollisionStay2D(Collision2D other)
    {

        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }

    }


    void OnCollisionExit2D(Collision2D other)
    {

        if (other.collider.tag == "Ground")
        {
            isGrounded = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Star") {
            myGameController.IncrementScore();
            Destroy(other.gameObject);
        }
    }
}
