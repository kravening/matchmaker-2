using UnityEngine;
using System.Collections;

public class TrampolineBounce : MonoBehaviour {

	public float speed;
	public float jumpSpeed; 
	public float gravity;
	public Vector3 moveDirection = Vector3.zero;
	public float downwardForce;
	public float terminalVelocity;
	public float airSpeed;

	bool isGrounded;

	void Update() 
	{
		// is the controller on the ground?
		if (isGrounded) 
		{
			//Feed moveDirection with input.
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);
			downwardForce = 0;
			//Multiply it by speed.
			moveDirection *= speed;

			//Jumping
			if (Input.GetButton ("Jump"))
			{
				downwardForce -= jumpSpeed;
			}   
		} 
		else 
		{
			if (downwardForce < terminalVelocity)
			{
				moveDirection *= airSpeed;
				downwardForce += gravity * Time.deltaTime;
			}
		}
		//Applying gravity to the controller
		moveDirection.y -= downwardForce * Time.deltaTime;
		//Making the character move
//		(moveDirection * Time.deltaTime);
	}
	void OnTriggerEnter(Collider collider)
	{           
		if (collider.tag == "smallTrampoline") 
		{
			downwardForce *= -1 * collider.gameObject.transform.GetComponent<TrampolineBounce> ().jumpSpeed;

		}
		if (collider.tag == "largeTrampoline") 
		{
			downwardForce *= -2 * collider.gameObject.transform.GetComponent<TrampolineBounce> ().jumpSpeed;
		}
	}
}