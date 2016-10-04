using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

	// Update is called once per frame
	void Update ()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var newRotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        if (newRotation.z > 0.4f)
        {
            newRotation = transform.rotation;
            newRotation.z = 0.4f;
            transform.rotation = newRotation;
        }
        if (newRotation.z < -0.4f)
        {
            newRotation = transform.rotation;
            newRotation.z = -0.4f;
            transform.rotation = newRotation;
        }

        transform.rotation = newRotation;
    }
}
