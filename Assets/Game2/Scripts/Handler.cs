using UnityEngine;
using System.Collections;

public class Handler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.SystemSetting;
		GameObject.DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
