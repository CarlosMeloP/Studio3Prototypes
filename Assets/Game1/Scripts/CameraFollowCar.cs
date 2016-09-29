using UnityEngine;
using System.Collections;
/// <summary>
/// Camera follow script which follows the [layer
/// </summary>
public class CameraFollowCar : MonoBehaviour {
	public Transform target;
	public float gap;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z);
	}
}
