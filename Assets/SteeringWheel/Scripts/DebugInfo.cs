using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugInfo : MonoBehaviour {
	public Text debugText;
	public SteeringWheel steeringWheelController;

	void Update () 
	{
		debugText.text = "Input value: " + steeringWheelController.GetInput() +    //Input value;
                         " Angle value: " + steeringWheelController.GetAngle() +   //Wheel angle;
                         " Pressed: " + steeringWheelController.isPressed();       //Pressed or not;
	}
}
