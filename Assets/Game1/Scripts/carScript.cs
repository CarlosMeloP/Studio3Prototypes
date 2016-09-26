using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class carScript : MonoBehaviour {

	[Tooltip("speed gaining")] public float acceleration;
	[Tooltip("maximum speed")] public float maxSpeed;

	// 0 - driving
	// 1 - shoot
	// 2 - collision

	Rigidbody2D rb; // rigidbody refference;

	int anDirection;

	float dustDistance; // distance between cat center and dust animation

	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

	public void move (Vector2 direction) // driving function
	{
		
		if (rb.velocity.magnitude <= maxSpeed) // if speed did not hit maximum
		{
			rb.velocity += direction * acceleration * Time.deltaTime; // gain speed
		} else // if speed hitted maximum
		{
			rb.velocity = direction * maxSpeed * Time.deltaTime; // redirect velocity to received direction
		}		


	}
   

}
