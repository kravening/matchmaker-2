using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
	CharacterController _cc;
	CollisionFlags collisionFlags;
	Camera _cam;
	PlayerAnimation _playerAnim;

	Vector3 _targetDirection;
	Vector3 _slideDirection;
	Vector3 _moveDirection = Vector3.zero;
	Vector2 _axis;

	//walk speedvv
	float _tempSpeed;
	float _targetSpeed = 20f;
	float _moveSpeed = 0f;
	float _speedIdleMax = 0.15f;
	float _speedIdleRotate = 1.2f;
	float _slideSpeed = 20f;

	float _speedInAir = 1f;
	float _currentSpeed = 20f;
	float _currentSmooth;

	//Smoothing values
	float _baseSpeedSmoothing = 10f;
	float _baseRotationSmoothing = 15f;
	float _baseAirSpeedSmoothing = 5;

	//Jump
	float _currentJumpHeight = 0f;
	float jumpComboTime = 1f;
	float jumpDelayTime = 0.1f;

	//Slide
	float slideThreshold = .95f;
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
	string _slideTag;
	//TODO use tag from tag class
	string _jumpFromWallObjectTag;
	//TODO use tag from tag class

	//Vectors
	Vector3 _playerStartPosition;
	Vector3 _inAirVelocity = Vector3.zero;
	Vector3 _wallJumpContactNormal;
	Vector3 _currentTouchingWall;
	Vector3 _previouslyJumpedFromWall;
	Vector3 _playerScale;

	//Quaternions
	Quaternion _playerStartRotation;

	//Transforms
	Transform _pushObject = null;
	Transform _grabObject = null;

	//Booleans
	bool _jump;
	bool _holdPreviousInput;
	bool _canWallJump;
	bool _isWallJumping;
	bool _wallJumpedRecently;
	bool _isSliding = true;
	bool _isOnIce = false;
	bool _isMoving = false;
	bool _isOnStickyPlatform = false;
	//LayerMasks
	LayerMask _pushLayers = -1;

	// Use this for initialization
	void Start ()
	{
		_cc = GetComponent<CharacterController> ();
		_cam = Camera.main.GetComponent<Camera> ();
		_jumpFromWallObjectTag = Tags.WALL;
		_slideTag = Tags.SLIDE;
		_playerScale = transform.localScale;
		_playerAnim = GetComponent<PlayerAnimation> ();
	}

	// Update is called once per frame
	void Update ()
	{
		collisionFlags = _cc.collisionFlags;
	}

	public void ProcessInput (Vector2 leftStick, bool aButton)
	{
		_axis = leftStick;

		if (_axis.x != 0 || _axis.y != 0) {
			_isMoving = true;
		} else {
			_isMoving = false;
		}

		_jump = aButton;
		Movement (_axis);
	}

	void Movement (Vector2 moveDir)
	{
		UpdateTargetDirection ();
		UpdateMoveDirection ();
		AngleSlide ();
		UpdateGravity ();
		JumpCheck ();
		CheckSurface ();
		//AngleSlide ();


		Vector3 movement = _moveDirection * _moveSpeed + new Vector3 (0, _verticalSpeed, 0) + _inAirVelocity; // stores direction with speed (h,v)
		movement *= Time.deltaTime;													// delta time for consistent speed
		if (_moveDirection != Vector3.zero) {
			if (_isMoving) {
				transform.rotation = Quaternion.LookRotation (_moveDirection);
			}
		}
		collisionFlags = _cc.Move (movement);

		if (_cc.isGrounded) { 														// character is on the ground (set rotation, translation, direction, speed)
			_inAirVelocity = new Vector3 (0, -0.5f, 0);								// turn off check on velocity, set to zero/// current set to -.1 because zero won't keep him on isGrounded true. goes back and forth			
			if (_moveSpeed < _speedIdleMax) {												// quick check on movespeed and turn it off (0), if it's
				_moveSpeed = 0;
				_playerAnim.SetBoolParameter ("isIdle",true);
				//idle
			} else {
				_playerAnim.SetBoolParameter ("isIdle",false);
				_playerAnim.SetFloatParameter ("moveSpeed", _moveSpeed);
				//moving on teh ground
				//spawn particles
				//play animation
				//more speedchecks for animations
				//check rotation for lean (relative to camera? & horizontal);
			}
			_playerAnim.SetBoolParameter ("isJumping", false);
		} else {
			//player is in the air
		}
		_holdPreviousInput = _jump;
	}

	void UpdateTargetDirection ()
	{
		Vector3 camForward = _cam.transform.TransformDirection (Vector3.forward);
		camForward.y = 0;
		camForward = camForward.normalized;
		Vector3 right = new Vector3 (camForward.z, 0, -camForward.x);
		float vertical = -_axis.y; 
		float horizontal = _axis.x;
		_targetDirection = horizontal * right + vertical * camForward;
	}

	void UpdateMoveDirection ()
	{
		if (_cc.isGrounded) {
			_currentSmooth = _baseSpeedSmoothing * Time.deltaTime;
			_wallJumpedRecently = false;
			_isWallJumping = false;
		} else {
			_inAirVelocity += _targetDirection.normalized * Time.deltaTime * _speedInAir;
		}

		if (!_wallJumpedRecently) { // rotate and move in air and ground, but can't rotate or move while walljumping (just time your jumps right)
			_moveDirection = Vector3.Lerp ( _moveDirection, _targetDirection, _baseRotationSmoothing * Time.deltaTime );
			_moveDirection = _moveDirection.normalized;

			_targetSpeed = Mathf.Min (_targetDirection.magnitude, 1);
		}
		_moveSpeed = Mathf.Lerp (_moveSpeed, _targetSpeed * _targetDirection.magnitude * _currentSpeed, _currentSmooth);
	}

	void UpdateGravity ()
	{
		if (_cc.isGrounded) {
			ResetGravity ();
		} else {
			_currentGravity += _gravityAcceleration * Time.deltaTime;
			_currentGravity = Mathf.Clamp (_currentGravity, 0, _maxGravity);
			_verticalSpeed -= _currentGravity * Time.deltaTime * 5;
		}
	}

	void JumpCheck ()
	{
		if (_cc.isGrounded) {
			Jump ();
			return;
		} else if (!_isWallJumping) {// walljump
			StartCoroutine ("WallJump");
			return;
		}
	}

	void Jump ()
	{
		if (_jump == true && _holdPreviousInput == false && !_isWallJumping) { // get button down
			_verticalSpeed = 25f; //the jump
			_playerAnim.SetBoolParameter("isJumping",true);
		}
	}

	void ResetGravity ()
	{
		_isWallJumping = false;
		_verticalSpeed = 0f;
		_currentGravity = 0f;
	}

	void OnControllerColliderHit (ControllerColliderHit hit) // for walljumping
	{
		if (hit.collider.tag == _jumpFromWallObjectTag) {
			_wallJumpContactNormal = hit.normal;  // used for rotating the player away from the wall
			_canWallJump = true;
			_currentTouchingWall = hit.gameObject.transform.position;
		}
	}

	IEnumerator WallJump ()
	{
		if (_jump && _holdPreviousInput == false  && _canWallJump == true) {
			_canWallJump = false;
			if (Math.Abs (_wallJumpContactNormal.y) < 0.2f && _previouslyJumpedFromWall != _currentTouchingWall) { // don't try to jump from the same wall twice
				_previouslyJumpedFromWall = _currentTouchingWall;
				_isWallJumping = true;
				ResetGravity ();
				_wallJumpContactNormal.y = 0;
				_moveDirection = _wallJumpContactNormal.normalized;
				_moveSpeed = _targetSpeed;
				_verticalSpeed = 30f; //the jump
				_wallJumpedRecently = true;
				_playerAnim.SetBoolParameter("isJumping",true);
			}
		}
		yield return new WaitForSeconds (.125f); //
		_isWallJumping = false;
	}
	void AngleSlide(){
		/*
			_slideDirection = Vector3.zero;
			RaycastHit hitInfo;
			if (Physics.Raycast (transform.position, Vector3.down, out hitInfo)) {

				if (hitInfo.collider.tag != _slideTag) {

					return;
				}
				if (hitInfo.normal.y <= slideThreshold) {
					_slideDirection = new Vector3 (hitInfo.normal.x, 0, hitInfo.normal.y);

				}
			_moveDirection += _slideDirection;
			}
			if (_slideDirection.magnitude < slideControllableSpeed) {
				//_moveDirection += _slideDirection;
			} else {
				//_moveDirection = _slideDirection; // no more control of movement
			}
			if (_slideDirection.magnitude > 0) {
				//_moveSpeed = _slideSpeed;
			}*/
		}
	void CheckSurface(){ // could be seperate class
		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, Vector3.down, out hitInfo, 1.51f)) {
			if (hitInfo.transform.tag == Tags.STICKYPLATFORM) {
				_isOnStickyPlatform = true;
				transform.parent = hitInfo.transform;
				return;
			}
		}
		_isOnStickyPlatform = false;
		transform.parent = null;
	}
}
