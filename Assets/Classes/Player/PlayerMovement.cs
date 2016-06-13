using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {
	CharacterController _cc;
	CollisionFlags collisionFlags;
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

	float _speedInAir = 1f;
	float _currentSpeed = 20f;
	float _currentSmooth;

	//Smoothing values
	float _baseSpeedSmoothing = 2.5f;
	float _baseSpeedRotation = 10f;
	float _baseSmoothDirection = 5f;
	float _baseAirSpeedSmoothing = 10f;

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
	float _walljumpDelay = 0.25f;

	//tags
	string _slideTag = "slide"; //TODO use tag from tag class
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
	bool _jump;
	bool _holdPreviousInput;
	bool _jumpableObject;
	bool _isWallJumping;

	//LayerMasks
	LayerMask _pushLayers = -1;

	// Use this for initialization
	void Start () {
		_cc = GetComponent<CharacterController> ();
		_cam = Camera.main.GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		collisionFlags = _cc.collisionFlags;
	}

	public void ProcessInput(Vector2 leftStick, bool aButton){
		_axis = leftStick;
		_jump = aButton;
		Movement (_axis);
	}

	void Movement(Vector2 moveDir){
		UpdateTargetDirection ();
		UpdateMoveDirection ();
		UpdateGravity ();
		JumpCheck();


		Vector3 movement = _moveDirection * _moveSpeed + new Vector3 ( 0, _verticalSpeed, 0 ) + _inAirVelocity; // stores direction with speed (h,v)
		movement *= Time.deltaTime;													// delta time for consistent speed
		if (_moveDirection != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation (_moveDirection);
		}
		_cc.Move ( movement );

		if (_cc.isGrounded) { 														// character is on the ground (set rotation, translation, direction, speed)
			_inAirVelocity = new Vector3 (0, -0.5f, 0);								// turn off check on velocity, set to zero/// current set to -.1 because zero won't keep him on isGrounded true. goes back and forth			
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
		if (_cc.isGrounded) {
			_moveDirection = _targetDirection;
			_moveDirection = _moveDirection.normalized;
			_currentSmooth = _baseSpeedSmoothing * Time.deltaTime;

			_targetSpeed = Mathf.Min (_targetDirection.magnitude, 1);
			_moveSpeed = Mathf.Lerp (_moveSpeed, _targetSpeed * _targetDirection.magnitude * _currentSpeed, _currentSmooth);
		} else {
			_inAirVelocity += _targetDirection.normalized * Time.deltaTime * _speedInAir;
		}
	}

	void UpdateGravity(){
		if (_cc.isGrounded) {
			ResetGravity ();
		} else {
			_currentGravity += _gravityAcceleration * Time.deltaTime;
			_currentGravity = Mathf.Clamp (_currentGravity, 0, _maxGravity);
			_verticalSpeed -= _currentGravity * Time.deltaTime * 10;
		}
	}

	void JumpCheck(){
		if (_cc.isGrounded) {
			Jump ();
		} else {// walljump
			WallJump();
		}
	}

	void Jump(){
			if (_jump == true && _holdPreviousInput == false){ // get button down
				_holdPreviousInput = _jump;
				_verticalSpeed = 25f; //the jump
			} else if(_jump == false && _holdPreviousInput == true) {
				_holdPreviousInput = _jump;
			}

	}

	void ResetGravity(){
		_verticalSpeed = 0f;
		_currentGravity = 0f;
	}

	void WallJump(){
		if (_jump && _jumpableObject == true) {
			_jumpableObject = false;
			_holdPreviousInput = _jump;
			_verticalSpeed = 0f;
			_currentGravity = 0f;
			if (Math.Abs (_objectJumpContactNormal.y) < 0.2f) {
				_verticalSpeed = 25f; //the jump
			}
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		Debug.DrawRay (hit.point,hit.normal);
		if (hit.collider.tag == _jumpFromObjectTag) {
			_jumpableObject = true;
		} else {
			_jumpableObject = false;
		}
		_objectJumpContactNormal = hit.normal;

	}

}
