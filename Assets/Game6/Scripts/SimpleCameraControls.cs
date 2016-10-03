using UnityEngine;
using System.Collections;

public class SimpleCameraControls : MonoBehaviour {
    public Transform target;
    public Vector2 offset = Vector2.zero;
    public float speed = 10.0f;

	void LateUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x,offset.y, transform.position.z), Time.deltaTime * speed);
	}
}
