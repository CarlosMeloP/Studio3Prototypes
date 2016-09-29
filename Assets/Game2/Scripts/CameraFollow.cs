using UnityEngine;
using System.Collections;
/// <summary>
/// Camera follow script which follows the [layer
/// </summary>
public class CameraFollow : MonoBehaviour {
	public Transform target;
	public float gap;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Camera.main.WorldToScreenPoint(target.transform.position).y>Screen.height/2)
		transform.position = new Vector3(transform.position.x,target.position.y + gap,transform.position.z);
	}
}
