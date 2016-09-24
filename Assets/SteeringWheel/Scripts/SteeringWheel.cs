using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SteeringWheel : MonoBehaviour {

	public Image steeringWheel;								//Steering wheel UI image;
	public float maxAngle = 500;							//Maximum (positive/negative) rotate angle;
	public float wheelFreeSpeed = 10;						//Wheel turn to default rotation speed;
	public float centerDeadZoneRadius = 5;					//Center dead zone radius;
	public float defaultAlpha = 0.5F, activeAlpha = 1.0F; 	//Wheel image alpha depending whether on pressed or not;
	public bool Interactable = true;						//Is steering wheel interactable or not;

	private RectTransform wheelRect;
	private CanvasGroup canvasGroup;
    private float wheelAngle;
	private float wheelTempAngle, wheelNewAngle;
	private bool isHold;
	private Vector2 wheelCenter, touchPos;
	private EventTrigger eventTrigger;
	
	void Start () 
	{
		wheelRect = steeringWheel.rectTransform;
		canvasGroup = steeringWheel.GetComponent<CanvasGroup> ();
		wheelCenter = wheelRect.position;
		canvasGroup.alpha = defaultAlpha;

		SetupListeners ();
	}	
	
	// Update is called once per frame
	void Update () {

     //   Debug.Log("Current input value: " + GetInput().ToString("F1") + "  Current angle value: " + GetAngle() + " Is Pressed: " + isPressed());

		if( isHold )
		{
			if(Interactable)
			{
				canvasGroup.alpha = activeAlpha;
				wheelNewAngle = Vector2.Angle( Vector2.up, touchPos - wheelCenter );
				
				// If mouse is very close to the steering wheel's center, do nothing
				if( Vector2.Distance( touchPos, wheelCenter ) > centerDeadZoneRadius )
				{
					if( touchPos.x > wheelCenter.x )
						wheelAngle += wheelNewAngle - wheelTempAngle;
					else
						wheelAngle -= wheelNewAngle - wheelTempAngle;
				}
				
				// Make sure that the wheelAngle does not exceed the maximumAngle
				if( wheelAngle > maxAngle )
					wheelAngle = maxAngle;
				else if( wheelAngle < -maxAngle )
					wheelAngle = -maxAngle;
				
				wheelTempAngle = wheelNewAngle;
			}
		}
		else 
		{
			canvasGroup.alpha = defaultAlpha;
			// If the wheel is rotated and not being held, rotate it to its default angle (zero)
			if( !Mathf.Approximately( 0f, wheelAngle ) )
			{
				float deltaAngle = wheelFreeSpeed;
				
				if( Mathf.Abs( deltaAngle ) > Mathf.Abs( wheelAngle ) )
				{
					wheelAngle = 0f;
					return;
				}
				
				if( wheelAngle > 0f )
					wheelAngle -= deltaAngle;
				else
					wheelAngle += deltaAngle;
			}
		}


		wheelRect.eulerAngles = new Vector3 (0, 0, -wheelAngle);
	}

	//Setup events;
	void SetupListeners()
	{
		eventTrigger = steeringWheel.gameObject.GetComponent<EventTrigger>();

		var a = new EventTrigger.TriggerEvent();
		a.AddListener( data => 
        {
			var evData = (PointerEventData)data;
			data.Use();
			
			isHold = true;
			touchPos = evData.position;
			wheelTempAngle = Vector2.Angle( Vector2.up, evData.position - wheelCenter );
		});
		
		eventTrigger.triggers.Add(new EventTrigger.Entry{callback = a, eventID = EventTriggerType.PointerDown});


		var b = new EventTrigger.TriggerEvent();
		b.AddListener( data => 
		              {
			var evData = (PointerEventData)data;
			data.Use();
			touchPos = evData.position;
		});
		
		eventTrigger.triggers.Add(new EventTrigger.Entry{callback = b, eventID = EventTriggerType.Drag});


		var c = new EventTrigger.TriggerEvent();
		c.AddListener( data => 
        {
			isHold = false;
		});
		
		eventTrigger.triggers.Add(new EventTrigger.Entry{callback = c, eventID = EventTriggerType.EndDrag});
	}


	//Returns Input value between -1 and 1 for your car controls;
	public float GetInput()
	{
		return Mathf.Round(wheelAngle / maxAngle * 100) /100;
	}

    //Returns rotation value;
    public int GetAngle()
    {
        return Mathf.FloorToInt(wheelAngle);
    }

    //Returns whether or not steering wheel is pressed;
    public bool isPressed()
    {
        return isHold;
    }
}
