using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {
	CharacterController _cc;

	Camera _cam;
	Vector3 _targetDirection;
	Vector3 _moveDirection = Vector3.zero;

	Vector2 _axis;

	//walk speedvv
	float _tempSpeed;
	float _targetSpeed = 20f;
	float _moveSpeed = 0f;
	float _speedIdleMax = 0.15f;
	float _speedIdleRotate = 1.2f;
	float _speedWalk = 3f;
	float _speedJog = 5f;
	float _speedRun = 8f;
	float _speedSprint = 12f;
	float _speedSlide = 3f;
	float _speedPush = 1.5f;
	float _speedGrab = 2f;
	float _speedJumpFromCrouch = 3f;
	float _speedJumpFromObject = 10f;
	float _speedCrouch = 0f;
	float _speedInAir = 1f;
	float _currentSpeed = 20f;
	float _currentSmooth;

	//Smoothing values
	float _baseSpeedSmoothing = 5f;
	float _baseSpeedRotation = 20f;
	float _baseSmoothDirection = 10f;

	//Jump
	float _currentJumpHeight = 0f;
	float _jump_1 = 25f; 
	float _jump_2 = 30f;
	float _jump_3 = 35f;
	float _jumpFromCrouch = 35f;
	float _jumpFromObject = 25f;
	float jumpComboTime = 1f;
	float jumpDelayTime = 0.1f;

	//Slide
	float slideThreshold = 0.88f;
	float slideControllableSpeed = 5f;

	//Push
	float _pushPower = 0.5f;

	//gravity
	float _currentGravity;
	float _maxGravity = 50f;
	float _gravityAcceleration = 50f;

	//---------------------------------------------------------

	//Floats
	float _controllerHeightDefault;
	float _controllerCenterYDefault;
	float _currentTime;
	float _verticalSpeed;

	//tags
	string slideTag = "slide"; //TODO use tag from tag class
	string _jumpFromObjectTag = "Wall"; //TODO use tag from tag class

	//Vectors
	Vector3 _playerStartPosition;
	Vector3 _inAirVelocity = Vector3.zero;
	Vector3 _objectJumpContactNormal;

	//Quaternions
	Quaternion _playerStartRotation;

	//Transforms
	Transform _pushObject = null;
	Transform _grabObject = null;

	//Booleans
	bool jump;
	bool holdPreviousInput;

	//LayerMasks
	LayerMask pushLayers = -1;

	// Use this for initialization
	void Start () {
		_cc = GetComponent<CharacterController> ();
		_cam = Camera.main.GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
	}

	public void ProcessInput(Vector2 leftStick, bool aButton){
		_axis = leftStick;
		jump = aButton;
		Movement (_axis);
	}

	void Movement(Vector2 moveDir){
		UpdateTargetDirection ();
		UpdateMoveDirection ();
		UpdateGravity ();
		Vector3 movement = _moveDirection * _moveSpeed + new Vector3 ( 0, _verticalSpeed, 0 ) + _inAirVelocity; // stores direction with speed (h,v)
		movement *= Time.deltaTime;													// delta time for consistent speed

		transform.rotation = Quaternion.LookRotation ( _moveDirection );
		_cc.Move ( movement );

		if (_cc.isGrounded) { 														// character is on the ground (set rotation, translation, direction, speed)
			_inAirVelocity = new Vector3 (0, -0.1f, 0);								// turn off check on velocity, set to zero/// current set to -.1 because zero won't keep him on isGrounded true. goes back and forth			
			if (_moveSpeed < _speedIdleMax) {												// quick check on movespeed and turn it off (0), if it's
				_moveSpeed = 0;
				//idle
			} else {
				//moving on teh ground
				//spawn particles
				//play animation
				//more speedchecks for animations
				//check rotation for lean (relative to camera? & horizontal);
			}
		} else {
			//player is in the air
		}

	}		

	void UpdateTargetDirection(){
		Vector3 camForward = _cam.transform.TransformDirection (Vector3.forward);
		camForward.y       = 0;
		camForward         = camForward.normalized;
		Vector3 right      = new Vector3 (camForward.z, 0, -camForward.x);
		float vertical   = -_axis.y; 
		float horizontal = _axis.x;
		_targetDirection = horizontal * right + vertical * camForward;
	}

	void UpdateMoveDirection(){
		//if (_targetDirection != Vector3.zero) {
			
			/*if(transform.rotation.y == 180 || transform.rotation.y == -180 || transform.rotation.y == 90 || transform.rotation.y == -90 ){
				int random = Random.Range (0,1);
				if (random >= 1) {
					transform.rotation.y += 0.1f;
				} else {
					transform.rotation.y -= 0.1f;
				}
			}*/
			//_moveDirection = Vector3.Lerp (_moveDirection, _targetDirection, _baseSmoothDirection * Time.deltaTime);
		_moveDirection = _targetDirection;
			_moveDirection = _moveDirection.normalized;
		//}
		_currentSmooth = _baseSpeedSmoothing * Time.deltaTime;

		_targetSpeed = Mathf.Min (_targetDirection.magnitude, 1);
		_moveSpeed = Mathf.Lerp (_moveSpeed, _targetSpeed * _targetDirection.magnitude * _currentSpeed, _currentSmooth);
		if (_cc.isGrounded) {

		} else {
			_inAirVelocity += _targetDirection.normalized * Time.deltaTime * _speedInAir;
		}
	}

	void UpdateGravity(){
		if (_cc.isGrounded) {
			_verticalSpeed = 0f;
			_currentGravity = 0f;
			JumpCheck();
		} else {
			_currentGravity += _gravityAcceleration * Time.deltaTime;
			_currentGravity = Mathf.Clamp (_currentGravity, 0, _maxGravity);
			_verticalSpeed -= _currentGravity * Time.deltaTime * 10;
		}
	}

	void JumpCheck(){
		if (jump == true && holdPreviousInput == false && _cc.isGrounded) { // get button down
			holdPreviousInput = true;
			_verticalSpeed = 25f;
		} else if(jump == false && holdPreviousInput == true) {
			holdPreviousInput = false;
		}
	}
}
