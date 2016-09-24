using UnityEngine;
using System.Collections;

public class CarPlayer : MonoBehaviour
{
	//public GUISkin skin;				//GUI skin
	public float moveSpeed;				//Move speed
	public float rotSpeed;				//Rotate speed
	public GameObject[] checkpoints;	//All checkpoints
	//public AudioClip audioCheckpoint;	//Checkpoint sound
	private int rounds = 0;				//Rounds
	private int nextCheckpoint = 1;		//Next checkpoint
	private bool win;					//Have we won

    public bl_Joystick Joystick;

    void Start ()
	{
		//Set screen orientation to landscape left
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Update ()
	{
		//Update
		MoveUpdate();
	}
	
	void MoveUpdate()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//If Horizontal is not 0
			if (Joystick.Horizontal != 0)
			{
				//If Vertical is not 0
				if (Joystick.Vertical != 0)
				{
					//Rotate the player
					transform.Rotate(0, Joystick.Horizontal * rotSpeed * 10 * Time.smoothDeltaTime,0);
				}
			}
			
			//Move the player
			GetComponent<Rigidbody>().AddForce(transform.forward * -Joystick.Vertical * (moveSpeed - 5) * 100 * Time.smoothDeltaTime);
		}
		
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		//If we are in a checkpoint trigger
		if (other.tag == "Checkpoint")
		{
			//If its name is checkpoint + nextCheckpoint
			if (other.name == "checkpoint " + nextCheckpoint.ToString())
			{
				//If its name is checkpoint 0 and nextCheckpoint is 0
				if (other.name == "checkpoint 0" && nextCheckpoint == 0)
				{
					//Add 1 to rounds
					rounds++;
					//If rounds is bigger than 3
					if (rounds >= 3)
					{
						//We won
						win = true;
					}
				}
				//Add 1 to nextCheckpoint
				nextCheckpoint++;
				//Play checkpoint sound
				//GetComponent<AudioSource>().clip = audioCheckpoint;
				//GetComponent<AudioSource>().Play();
				//If nextCheckpoint is bigger than checkpoints.Length - 1
				if (nextCheckpoint > checkpoints.Length - 1)
				{
					//Set nextCheckpoint to 0
					nextCheckpoint = 0;
				}
			}
		}
	}

}
