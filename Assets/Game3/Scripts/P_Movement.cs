using UnityEngine;
using System.Collections;

public class P_Movement : MonoBehaviour
{
    private int xPos;
    private float t = 0.1f;
    public bool isTouch=true;

	// Update is called once per frame
	void Update ()
    {
        if (isTouch == true)
        {
            //Touch Control 
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).position.x > Screen.width / 2 && t > 0.075f && xPos < 7)
                {
                    xPos++;
                    t = 0.0f;
                }
                if (Input.GetTouch(0).position.x < Screen.width / 2 && t > 0.075f && xPos < -7)
                {
                    xPos--;
                    t = 0.0f;
                }
                t += Time.deltaTime;
            }
            else
            {

                //Keyboard Controls
                if (Input.GetButton("Horizontal"))
                {
                    if (Input.GetAxis("Horizontal") > 0 && t > 0.075f && xPos < 7)
                    {
                        xPos++;
                        t = 0.0f;
                    }
                    if (Input.GetAxis("Horizontal") < 0 && t > 0.075f && xPos > -7)
                    {
                        xPos--;
                        t = 0.0f;
                    }
                    t += Time.deltaTime;
                }
            }
            MovePlayer();
        }


    }

    void MovePlayer()
    { 
        Vector2 playerPos = gameObject.transform.position;
        playerPos.x = xPos;
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, playerPos, 10 * Time.deltaTime);
    }
}
