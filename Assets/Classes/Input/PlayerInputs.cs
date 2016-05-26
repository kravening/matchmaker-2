using UnityEngine;
using System.Collections;

public class PlayerInputs : MonoBehaviour {

   

    void Start()
    {

    }

	void Update () 
    {
        XboxControllerInput();
    }


    void XboxControllerInput()
    {
        //DPAD
        float dpadX = Input.GetAxis(InputAxes.DPADX); //DPAD X AXIS

        if (dpadX > 0)
        {

        }
        else if (dpadX < 0)
        {

        }

        float dpadY = Input.GetAxis(InputAxes.DPADY); //DPAD Y AXIS

        if (dpadY > 0)
        {

        }
        else if (dpadY < 0)
        {

        }

        //ANALOG STICKS
        float leftX = Input.GetAxis(InputAxes.LEFTX); //LEFT ANALOG X AXIS
        float leftY = Input.GetAxis(InputAxes.LEFTY); //LEFT ANALOG Y AXIS

        Vector3 inputVector = new Vector3(Input.GetAxis(InputAxes.LEFTX),0, -Input.GetAxis(InputAxes.LEFTY));

        if (leftX != 0 || leftY != 0)
        {
           
        }

        float rightX = Input.GetAxis(InputAxes.RIGHTX); //RIGHT ANALOG X AXIS
        float rightY = Input.GetAxis(InputAxes.RIGHTY); //RIGHT ANALOG X AXIS
        
        if (rightX != 0)
        {
            
        }

        if (rightY != 0)
        {
            
        }

        if (Input.GetButtonDown(InputAxes.L3))
        {

        }

        if (Input.GetButtonDown(InputAxes.R3))
        {

        }

        //FACE BUTTONS
        if (Input.GetButtonDown(InputAxes.A))
        {

        }

        if (Input.GetButtonDown(InputAxes.B))
        {

        }
        
        if (Input.GetButtonDown(InputAxes.X))
        {
         
        }
        if (Input.GetButtonDown(InputAxes.Y))
        {

        }

        //BUMPERS & TRIGGERS

        //BUMPERS
        if (Input.GetButton(InputAxes.LB))
        {

        }
        if (Input.GetButtonDown(InputAxes.RB))
        {

        }

        //TRIGGERS
        float leftTrigger = Input.GetAxis(InputAxes.LT);
        float rightTrigger = Input.GetAxis(InputAxes.RT);

        if (leftTrigger > 0)
        {

        }
        if (rightTrigger > 0)
        {

        }

        //START & BACK
        if (Input.GetButtonDown(InputAxes.START))
        {

        }
        if (Input.GetButtonDown(InputAxes.BACK))
        {

        }

        //Idle
        if (!Input.anyKeyDown && leftY == 0 && leftX == 0)
        {

        }
    }
}
