using UnityEngine;
using System.Collections;

public class Game7_Player : MonoBehaviour
{
	public GameObject mesh;			//Mesh
	public float speed;				//Move speed
	private bool dead;				//Are we dead
	private bool win;				//Has we won
	private bool hitHole;			//Are we in a hole
	private GameObject hole;		//The hole we are in
	
	void Start ()
	{
		//Set screen orientation portrait
		Screen.orientation = ScreenOrientation.Portrait;
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Update ()
	{
		//If we are not dead and we are not in a hole and we has not won
		if (!dead && !hitHole && !win)
		{
			//Update
			MoveUpdate();
		}
		//If we are not dead and we are in a hole and we has not won
		else if(!dead && hitHole && !win)
		{
			//Set direction to the hole
			Vector3 dir = transform.position - hole.transform.position;
			//Add force to the ball
			GetComponent<Rigidbody>().AddForce(-dir * speed * Time.deltaTime);
			
			//Make the ball small
			transform.localScale -= new Vector3(0.1f,0.1f,0.1f) * Time.smoothDeltaTime;
			
			//If the ball scale is less than 0.2
			if (transform.localScale.x < 0.2f)
			{
				//Dont show ball
				mesh.GetComponent<Renderer>().enabled = false;
				//Kill
				dead = true;
                Application.LoadLevel("Game 7");
            }
		}
	}
	
	void MoveUpdate()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//AddForce (x force = Horizontal) (z force = Vertical)
			GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * speed * Time.deltaTime);
		}
		//If the game is running on a android device
		else
		{
			//AddForce (x force = acceleration.y) (z force = acceleration.x)
			GetComponent<Rigidbody>().AddForce(new Vector3(-Input.acceleration.y,0,Input.acceleration.x) * speed * 5 * Time.deltaTime);
		}	
	}
	
	void OnTriggerEnter(Collider other)
	{
		//If we are not in a hole and we are in a enemy trigger
		if (!hitHole && other.tag == "Enemy")
		{

			//Set hole to the hole we has hit
			hole = other.gameObject;
			//Set hitHole to true
			hitHole = true;
		}
		//If we are in a win trigger
		else if (other.tag == "Win")
		{
			//We won the game
			win = true;
		}
	}
	
	
}
