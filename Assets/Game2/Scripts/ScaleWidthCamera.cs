using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScaleWidthCamera : MonoBehaviour
{
    public enum ScaleMode
    {
        ScaleToWidth,
        ScaleToHeight
    }
    public int targetWidth = 640;
    public int targetHeight = 1089;
    public float pixelsToUnits = 100;

    public ScaleMode scaleMode;
	
    void Start()
    {
        targetHeight = Screen.height;
        targetWidth = Screen.width;
        if(Screen.orientation== ScreenOrientation.Portrait)
        {
            scaleMode = ScaleMode.ScaleToWidth;
        }
        else
        {
            scaleMode = ScaleMode.ScaleToHeight;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if(scaleMode==ScaleMode.ScaleToHeight)
        {
            pixelsToUnits = targetHeight / 10;
            int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);

        Camera.main.orthographicSize = height / pixelsToUnits /2;

        }

        if (scaleMode == ScaleMode.ScaleToWidth)
        {
            pixelsToUnits = targetWidth / 10;
            int width = Mathf.RoundToInt(targetHeight / (float)Screen.height * Screen.width);

            Camera.main.orthographicSize = width / pixelsToUnits/2 ;
        }

    }
}
