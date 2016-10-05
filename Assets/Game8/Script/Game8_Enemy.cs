using UnityEngine;
using System.Collections;

public class Game8_Enemy : MonoBehaviour
{
	public float moveSpeed;			//Move speed
	public float attackSpeed;		//Attack speed
	public int damage;				//Damage
	private float tmpAttackSpeed;	//Tmp attack speed
	public GameObject dead;			//Are we dead
	private Transform player;		//The player
	private int hp = 100;			//Health
	
	void Start ()
	{
		//Find player
		player = GameObject.Find("Player").transform;
	}
	
	void Update ()
	{
		//If the distance between the player position and the our position is over 1 meter
		if (Vector3.Distance(player.position,transform.position) > 1)
		{
			//Update
			MoveUpdate();
		}
		else
		{
			//If tmpAttackSpeed is bigger than attackSpeed
			if (tmpAttackSpeed > attackSpeed)
			{
				//Set tmpAttackSpeed to 0
				tmpAttackSpeed = 0;
				//Hit the player
				player.GetComponent<Game8_Player>().Hit(damage);
			}
			else
			{
				//Add 1 to tmpAttackSpeed
				tmpAttackSpeed += 1 * Time.deltaTime;
			}
		}
	}
	
	void MoveUpdate()
	{
		//Move enemy
		transform.Translate(Vector3.forward * moveSpeed * Time.smoothDeltaTime);
		//Rotate enemy
		transform.rotation = Quaternion.LookRotation(player.position - transform.position);	
	}
	
	public void Hit(int _damage)
	{

		//Remove the damage value from the health
		hp -= _damage;
		//If health is less than 0
		if (hp <= 0)
		{
			//Destroy
			Destroy(gameObject);
		}
	}
}
