using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour {

	public Transform currentMount; 
	public float SpeedFactor = 0.1f; 

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{
		transform.position = Vector3.Lerp (transform.position, currentMount.position, SpeedFactor );
		transform.rotation = Quaternion.Slerp (transform.rotation, currentMount.rotation, SpeedFactor);
	}

	public void setmount (Transform newMount)
	{
		currentMount = newMount;
	}
}