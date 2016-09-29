using UnityEngine;
using System.Collections;

public class Game_Init : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        P_Collide.heatlh = 3;
        P_Collide.tokens = 0;
        DestroyMe.timeToDestroy = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
