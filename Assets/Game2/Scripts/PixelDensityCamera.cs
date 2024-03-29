﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelDensityCamera : MonoBehaviour
{

    public float pixelsToUnits = 100;

    public float orthographicSize = 5;
    public float aspect = 1.33333f;

    void Start()
    {
        Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }

	// Update is called once per frame
/*	void Update ()
    {
	    Camera.main.orthographicSize = Screen.height / pixelsToUnits / 2;
	}*/
}
