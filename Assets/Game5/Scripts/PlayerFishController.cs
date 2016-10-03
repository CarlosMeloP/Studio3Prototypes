
using UnityEngine;
using System.Collections;

public class PlayerFishController : MonoBehaviour
{
private Vector2 velosityObject  = Vector2.zero;
public GameObject cam;
private bool  empty = true;

    private Quaternion r;
    private Vector3 t;

    void Awaker()
    {
        r = this.transform.rotation;
        t = this.transform.position;
    }
    void Start()
    {
        this.transform.rotation = r;
        this.transform.position = t;
    }

        void  Update ()
         {

            //turn the fish so that it is not overturned
            if(gameObject.transform.rotation.z < 0.7f && gameObject.transform.rotation.z > -0.7f){
            r.z = velosityObject.y / 10;
            }
            if(gameObject.transform.rotation.z > 0.7f){
            r.z -= 0.1f;
            }
            if(gameObject.transform.rotation.z < -0.7f){
            r.z += 0.1f;
            }
            //we get fish speed
            velosityObject = gameObject.GetComponent<Rigidbody2D>().velocity;
            //camera monitors the fish
            cam.transform.position =  new Vector3(gameObject.transform.position.x + 1.5f,cam.transform.position.y,cam.transform.position.z);
            //asking fish movement along the x-axis
            t.x += 0.08f;
            //the lower the fish the greater the buoyancy force
            if(gameObject.transform.position.y < 0){
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,3 * -gameObject.transform.position.y));
            }
            //if you pressed the up arrow and the fish is at the top of the screen
            if(Input.GetKey(KeyCode.UpArrow) && gameObject.transform.position.y < 0){
            //if the speed of the object is less than 10
            if(velosityObject.y < 10){
            //add force y-axis
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,25));
            }
            }
            //if you pressed the down arrow
            if(Input.GetKey(KeyCode.DownArrow)){
            //if the speed of the object is less than 10
            if(velosityObject.y > -10)
            {
            //add force y-axis
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-25));
            }

            }


        foreach (Touch touch in Input.touches) {
            //if touch is at the top of the screen and the subject is at the bottom of the screen
            if(touch.position.y > cam.GetComponent<Camera>().pixelHeight / 2 && gameObject.transform.position.y < 0){
            //if the speed of the object is less than 10
            if(velosityObject.y < 10){
            //add force y-axis
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,25));
            }
            }
            //If touch is at the bottom of the screen
            if(touch.position.y < cam.GetComponent<Camera>().pixelHeight / 2){
            //if the speed of the object is less than 10
            if(velosityObject.y > -10){
            //add force y-axis
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-25));
            }
          }
        }
     }
}