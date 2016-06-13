using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {
	[SerializeField]bool _isGrounded;
	public bool isGrounded{get{return _isGrounded; }}

	void OnTriggerEnter(Collider other){
		if (other.tag != Tags.PLAYER) {
			_isGrounded = true;
		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag != Tags.PLAYER) {
			_isGrounded = false;
		}
	}
}
