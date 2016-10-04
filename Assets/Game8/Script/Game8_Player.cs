using UnityEngine;
using System.Collections;

public class Game2_Player : MonoBehaviour
{

	public GameObject mesh;						//Mesh
	public GameObject cameraGO;					//Main Camera
	public float moveSpeed;						//Move speed
	public int hp = 100;						//Health
	public float texUpdateTime;					//Textures uodate time
	private float tmpTexUpdateTime;				//Tmp textures uodate time
	private int selectedTex;					//Selected Textures
	public GameObject bullet;					//Bullet prefab
	public float fireTime;						//Fire time
	private float tmpFireTime;					//Tmp fire time
	public Transform spawnBulletPosition;		//Bullet Spawn position
	private bool dead;							//Are we dead
	private Vector3 dir;						//The move direction
	private Vector3 lookDir;					//The look direction
	private Vector3 tempVector;					//We need this to figure out where to look
	private Vector3 tempVector2;				//We need this to figure out where to look
	private GameObject leftJoystick;			//The left joystick
	private GameObject rightJoystick;			//The right joystick
	
	void Start ()
	{
		//Set the screen orientation to landscape left
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Find left joystick
		leftJoystick = GameObject.Find("Left Joystick");
		//Find right joystick
		rightJoystick = GameObject.Find("Right Joystick");
		//Start SetupJoysticks
		StartCoroutine("SetupJoysticks");
		//Set sleep time to nerver
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

	}
	
	void Update ()
	{
		//Update
		MoveUpdate();
		//Set camare position
		cameraGO.transform.position = new Vector3(transform.position.x,10,transform.position.z);
	}
	
	void MoveUpdate()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//Horizontal axis and Vertical axis is not 0
			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			{
				//Set dir x to Horizontal and dir z to Vertical
				dir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
			}
			else
			{
				//Set dir to 0
				dir = new Vector3(0,0,0);
			}
			//Find the center of the screen
			tempVector2 = new Vector3(Screen.width * 0.5f,0,Screen.height * 0.5f);
			//Get mouse position
			tempVector = Input.mousePosition;
			//Set tempVector z to tempVector.y
			tempVector.z = tempVector.y;
			//Set tempVector y to 0
			tempVector.y = 0;
			//Set lookDir to  tempVector - tempVector2
			lookDir = tempVector - tempVector2;
			
			//If left mouse click
			if (Input.GetMouseButton(0))
			{
				//tmpFireTime is bigger than fireTime
				if (tmpFireTime >= fireTime)
				{
					//Set tmpFireTime to 0
					tmpFireTime = 0;
					
					//Instantiate bullet
					GameObject go = Instantiate(bullet, spawnBulletPosition.position, Quaternion.LookRotation(lookDir)) as GameObject;
					//Ignore collision with other bullets
					Physics.IgnoreCollision(go.GetComponent<Collider>(), GetComponent<Collider>());
					
				}
				//If tmpFireTime less than fireTime
				else if (tmpFireTime < fireTime)
				{
					//Add 1 to tmpFireTime
					tmpFireTime += 1 * Time.deltaTime;
				}
			}
		}
		//If the game is running on a android device
		else
		{
			//Get left joystick x position
			float mX = leftJoystick.GetComponent<Joystick>().position.x;
			//Get left joystick y position
			float mY = leftJoystick.GetComponent<Joystick>().position.y;
			//If joystick x position and left joystick y position is not 0
			if (mX != 0 || mY != 0)
			{
				//Set dir x to joystick x position and dir z joystick y position
				dir = new Vector3(mX,0,mY);

			}
			else
			{
				//Set dir to 0
				dir = new Vector3(0,0,0);
			}
			
			//Get right joystick x position
			float lX = rightJoystick.GetComponent<Joystick>().position.x;
			//Get right joystick y position
			float lY = rightJoystick.GetComponent<Joystick>().position.y;
			//If joystick x position and left joystick y position is not 0
			if (lX != 0 || lY != 0)
			{
				//Set lookDir x to joystick x position and dir z joystick y position
				lookDir = new Vector3(lX,0,lY);
				
				//If tmpFireTime is bigger than fireTime
				if (tmpFireTime >= fireTime)
				{
					//Set tmpFireTime to 0
					tmpFireTime = 0;
					
					//Spawn bullet
					GameObject go = Instantiate(bullet, spawnBulletPosition.position, Quaternion.LookRotation(lookDir)) as GameObject;
					//Ignore collision with other bullets
					Physics.IgnoreCollision(go.GetComponent<Collider>(), GetComponent<Collider>());
				}
				//If tmpFireTime is less than fireTime
				else if (tmpFireTime < fireTime)
				{
					//Add 1 to tmpFireTime
					tmpFireTime += 1 * Time.deltaTime;
				}
			}
		}
		
		//Move player
		transform.Translate(dir * moveSpeed * Time.smoothDeltaTime,Space.World);
		//Rotate player
		transform.rotation = Quaternion.LookRotation(lookDir);	
	}
	
	
	public void Hit(int _damage)
	{
		
		//Remove damge value form health
		hp -= _damage;
		//If health is less than 0
		if (hp <= 0)
		{
			
			//We are dead
			dead = true;
			//Set time scale to 0
			Time.timeScale = 0;
            Application.LoadLevel("Game 2");
        }
	}
	
	IEnumerator SetupJoysticks()
	{
		//Set joystick position
		leftJoystick.transform.position = new Vector3(0.2f,0.2f,0);
		rightJoystick.transform.position = new Vector3(0.8f,0.2f,0);
		
		//Wait 1 seconds
		yield return new WaitForSeconds(1);
		
		//Start joystick
		leftJoystick.GetComponent<Joystick>().StartGame();
		rightJoystick.GetComponent<Joystick>().StartGame();
	}
}
