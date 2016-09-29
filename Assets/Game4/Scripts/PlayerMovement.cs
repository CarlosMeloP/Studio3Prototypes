using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private float MaxSpeed = 5.0f;
    private Vector3 input;
    private Vector3 spawn;
    private Rigidbody rb;

    public GameObject deathParticle;
    [SerializeField] private bl_Joystick Joystick;

    // Use this for initialization
    void Start ()
    {
        Joystick = GameObject.Find ("Joystick").GetComponent<bl_Joystick>();
        rb = this.GetComponent<Rigidbody>();
        spawn = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*   input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
           if(rb.velocity.magnitude<MaxSpeed)
           { 
           rb.AddForce(input*moveSpeed);
           }*/

        float v = Joystick.Vertical; //get the vertical value of joystick
        float h = Joystick.Horizontal;//get the horizontal value of joystick

        //in case you using keys instead of axis (due keys are bool and not float) you can do this:
        //bool isKeyPressed = (Joystick.Horizontal > 0) ? true : false;

        //ready!, you not need more.
        Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * moveSpeed;
        transform.Translate(translate);

        if (transform.position.y<-2)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag=="Enemy")
        {
            Die();
            
        }
  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Goal")
        {
            GameManager.CompleteLevel();
            print("Level Complete");
        }
    }

    void Die()
    {
        Instantiate(deathParticle, this.transform.position, Quaternion.identity);
        transform.position = spawn;
    }
}
