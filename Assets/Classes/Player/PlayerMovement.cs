using UnityEngine;
using System.Collections;
struct Surface
{
	
}
public class PlayerMovement : MonoBehaviour {
	private CharacterController _cc;
	private float _maxMovementSpeed;
	private float _celerationSpeed;
	private Vector3 _cameraForwardVector;
	private float currentSpeed = 15;
	private float maxSpeed;
	private float downForce;
	private float gravityStrength;
	private float addedForce;
	private float addedForceFalloff;
	// Use this for initialization
	void Start () {
		_cc = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ProcessInput(Vector2 leftStick, bool aButton){
		Move (leftStick);
	}

	void Move(Vector2 moveDir){
		if (!_cc.isGrounded) {
			_cc.Move (new Vector3 (0, downForce, 0));
		}
		_cc.Move (new Vector3 (moveDir.x,0,moveDir.y));
	}

	void Jump(){

	}

	void NormalJump(){

	}

	void WallJump(){

	}

	void CheckSurface(){

	}

	void UpdateForward(){
		_cameraForwardVector = Camera.main.transform.forward;
	}

	void UpdateDownForce(){
		//downForce =
	}
}
