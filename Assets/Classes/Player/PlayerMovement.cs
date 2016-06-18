using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
	CharacterController _cc;
	CollisionFlags collisionFlags;
	Camera _cam;
	Animator _animator;

	Vector3 _targetDirection;
	Vector3 _slideDirection;
	Vector3 _moveDirection = Vector3.zero;
	Vector2 _axis;

	//walk speedvv
	float _tempSpeed;
	float _targetSpeed = 15f;
	float _moveSpeed = 0f;
	float _speedIdleMax = 0.1f;
	float _speedIdleRotate = 1.2f;
	float _slideSpeed = 15f;

	float _speedInAir = 1f;
	float _currentSpeed = 15f;
	float _currentSmooth;

	//Smoothing values
	float _baseSpeedSmoothing = 20f;
	float _baseRotationSmoothing = 15f;
	float _currentSpeedSmoothing;
	float _currentRotationSmoothing;

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
	float _maxGravity = 40f;
	float _gravityAcceleration = 50f;

	//---------------------------------------------------------

	//Floats
	float _controllerHeightDefault;
	float _controllerCenterYDefault;
	float _currentTime;
	float _verticalSpeed;
	float _walljumpDelay = 0.25f;
	float _animSpeed;
	float _baseAnimSpeedMultiplier = 3;
	float _animSpeedMultiplier;

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
	bool _onSlide = false;
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
		_animator = GetComponent<Animator> ();
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
		SurfaceCheck ();
		UpdateTargetDirection ();
		SurfaceCheck ();
		UpdateMoveDirection ();
		SurfaceCheck ();
		AngleSlide ();
		SurfaceCheck ();
		UpdateGravity ();
		SurfaceCheck ();
		JumpCheck ();
		SurfaceCheck ();
		//AngleSlide ();


		Vector3 movement = _moveDirection * _moveSpeed + new Vector3 (0, _verticalSpeed, 0) + _inAirVelocity; // stores direction with speed (h,v)
		movement *= Time.deltaTime;													// delta time for consistent speed
		if (_moveDirection != Vector3.zero) {
			if (_isMoving) {
				transform.rotation = Quaternion.LookRotation (_moveDirection);
			}
		}
		collisionFlags = _cc.Move (movement);
		_animator.SetBool ("isOnGround", _cc.isGrounded);
		if (_cc.isGrounded) { 														// character is on the ground (set rotation, translation, direction, speed)
			_inAirVelocity = new Vector3 (0, -0.5f, 0);							// turn off check on velocity, set to zero/// current set to -.1 because zero won't keep him on isGrounded true. goes back and forth
			_animator.SetBool ("isIdle",false);
			if (_moveSpeed < _speedIdleMax) {												// quick check on movespeed and turn it off (0), if it's
				_moveSpeed = 0;
				_animator.SetBool ("isIdle",true);
				//idle
			} else {
				_animSpeed = (_moveSpeed / _currentSpeed) * _animSpeedMultiplier;
				_animator.SetFloat ("moveSpeed", _moveSpeed);
				_animator.SetFloat ("animSpeed", _animSpeed);
				//moving on teh ground
				//spawn particles
				//play animation
				//more speedchecks for animations
				//check rotation for lean (relative to camera? & horizontal);
			}
			_animator.SetBool ("isJumping", false);
			_previouslyJumpedFromWall = new Vector3 (0,0,0);
		} else {
			_animator.SetBool ("isIdle",false);
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
			_currentSmooth = _currentSpeedSmoothing * Time.deltaTime;
			_wallJumpedRecently = false;
			_isWallJumping = false;
		} else {
			_inAirVelocity += _targetDirection.normalized * Time.deltaTime * _speedInAir;
		}

		if (!_wallJumpedRecently) { // rotate and move in air and ground, but can't rotate or move while walljumping (just time your jumps right)
			_moveDirection = Vector3.Lerp ( _moveDirection, _targetDirection, _currentRotationSmoothing * Time.deltaTime );
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
			StartCoroutine ("SetJumpAnim");
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
				_verticalSpeed = 30f;
				_wallJumpedRecently = true;
				StartCoroutine ("SetJumpAnim");
			}
		}
		yield return new WaitForSeconds (.125f); //
		_isWallJumping = false;
	}

	IEnumerator SetJumpAnim(){
		_animator.SetBool("once",true);
		_animator.SetBool ("isJumping",true);
		yield return new WaitForSeconds (.15f);
		_animator.SetBool("once",false);
		yield return new WaitForSeconds (.75f);
		_animator.SetBool("isJumping",false);
	

	}
	void AngleSlide(){/*
			_slideDirection = Vector3.zero;
			RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, Vector3.down, out hitInfo, 3f)) {

				if (hitInfo.normal.y <= slideThreshold && hitInfo.collider.tag == _slideTag) {
				
				}else{
					_slideDirection = new Vector3 (0, 0, 0);
					//return;
				}
				//_moveDirection += _slideDirection;
			}

			if (_slideDirection.magnitude < slideControllableSpeed) {
				_moveDirection += _slideDirection;
			} else {
				_moveDirection = _slideDirection; // no more control of movement
			}
			if (_slideDirection.magnitude > 0) {
				_moveSpeed = _slideSpeed;
			}*/
		}
	void SurfaceCheck(){
		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, Vector3.down, out hitInfo, 4f)) {
			if (hitInfo.collider.tag == Tags.ICEFLOOR && _cc.isGrounded) {
				_currentRotationSmoothing = 4;
				_currentSpeedSmoothing = 1;
				_animSpeedMultiplier = _baseAnimSpeedMultiplier / 2;
			} else {
				_animSpeedMultiplier = _baseAnimSpeedMultiplier;
				_currentRotationSmoothing = _baseRotationSmoothing;
				_currentSpeedSmoothing = _baseSpeedSmoothing;
			}
		}
	}

}
