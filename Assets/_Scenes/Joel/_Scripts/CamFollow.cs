using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private float smoothing = 10f;

	Vector3 offset;

	void Start ()
	{
		offset = transform.position - target.position;
	}

	
	void Update () 
	{
		//transform.LookAt(target.transform);

		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position,targetCamPos,smoothing * Time.deltaTime);
	}
}
