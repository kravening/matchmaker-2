using UnityEngine;
using System.Collections;

public class PlayerInputs : MonoBehaviour {
	private PlayerMovement _playerMovement;

	//DPAD
	float dPadX; //DPAD X AXIS
	float dPadY; //DPAD Y AXIS

	//ANALOG STICKS
	float leftX; //LEFT ANALOG X AXIS
	float leftY; //LEFT ANALOG Y AXIS
	float rightX; //RIGHT ANALOG X AXIS
	float rightY; //RIGHT ANALOG Y AXIS

	//TRIGGERS
	float leftTrigger;
	float rightTrigger;

	//BUTTONS
	bool aButton;
	bool bButton;
	bool yButton;
	bool xButton;

	Vector2 leftStickVector = new Vector2(0,0);

	//MODIFIERS
	float deadzone = 0.25f;
	void Start(){
		_playerMovement = GetComponent<PlayerMovement> ();
	}
	void Update () 
    {
        UpdateInput();
    }


    void UpdateInput()
    {
        //DPAD
        dPadX  = Input.GetAxisRaw(InputAxes.DPADX); //DPAD X AXIS
		dPadY  = Input.GetAxisRaw(InputAxes.DPADY); //DPAD Y AXIS

		//ANALOG STICKS
		leftX  = Input.GetAxisRaw(InputAxes.LEFTX); //LEFT ANALOG X AXIS
		leftY  = Input.GetAxisRaw(InputAxes.LEFTY); //LEFT ANALOG Y AXIS
		rightX = Input.GetAxisRaw(InputAxes.RIGHTX); //RIGHT ANALOG X AXIS
		rightY = Input.GetAxisRaw(InputAxes.RIGHTY); //RIGHT ANALOG Y AXIS

		if (leftX >= deadzone || leftX <= -deadzone) {
			leftStickVector.x = leftX;
		} else {
			leftStickVector.x = 0;
		}
		if (leftY >= deadzone || leftY <= -deadzone) {
			leftStickVector.y = leftY;
		} else {
			leftStickVector.y = 0;
		}

		//TRIGGERS
		leftTrigger = Input.GetAxisRaw(InputAxes.LT);
		rightTrigger = Input.GetAxisRaw(InputAxes.RT);

		//BUTTONS
		aButton = Input.GetButton (InputAxes.A);
		bButton = Input.GetButton (InputAxes.B);
		yButton = Input.GetButton (InputAxes.Y);
		xButton = Input.GetButton (InputAxes.X);

		_playerMovement.ProcessInput (leftStickVector, aButton);
    }
}
