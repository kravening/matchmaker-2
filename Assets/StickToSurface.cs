using UnityEngine;
using System.Collections;

public class StickToSurface : MonoBehaviour {
	void Update () {
		CheckForSurface ();
	}
	void CheckForSurface(){ // could be seperate class
		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, Vector3.down, out hitInfo, 2.1f)) {
			if (hitInfo.transform.tag == Tags.STICKYPLATFORM) {
				transform.parent = hitInfo.transform;
				return;
			}
		}
		transform.parent = null;
	}
}
