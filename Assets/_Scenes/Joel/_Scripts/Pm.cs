using UnityEngine;
using System.Collections;

public class Pm : MonoBehaviour {

	public float speed;

	private Rigidbody _rigidbidy;
	private float xInput;
	private float yInput;

	void Awake() 
	{
		_rigidbidy = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		xInput = Input.GetAxis ("Horizontal");
		yInput = Input.GetAxis ("Vertical");

//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		Plane plane = new Plane (Vector3.up, Vector3.zero);
//		float distance;
//
//		if (plane.Raycast (ray, out distance)) 
//		{
//			Vector3 point = ray.GetPoint (distance);
//			Vector3 heightAdjustedPoint = new Vector3 (point.x,
//				transform.position.y,
//				point.z);
//			transform.LookAt (heightAdjustedPoint);
//		}
	}

	void FixedUpdate()
	{
		Vector3 direction = new Vector3 (xInput, 0f, yInput).normalized;
		Vector3 velocity = direction * speed * Time.fixedDeltaTime;
		_rigidbidy.MovePosition (_rigidbidy.position + velocity);
	}
}
