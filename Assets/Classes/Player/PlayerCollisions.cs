using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {
	private Transform currentPlatform;
	void OnCollisionEnter(Collision coll)
    {
		Debug.Log (coll.gameObject.tag);
		if (coll.gameObject.tag == Tags.FALLINGPLATFORM)
        {
			FallingPlatform fallingPlatform = coll.gameObject.GetComponent<FallingPlatform>();
			fallingPlatform.StartFall();
        }
		if (coll.gameObject.tag == Tags.PLATFORM || coll.gameObject.tag == Tags.STICKYPLATFORM)
		{
			currentPlatform = coll.transform;
			this.gameObject.transform.parent = coll.transform;
		}
    }

	void OnCollisionExit(Collision coll)
    {
		if (coll.gameObject.tag == Tags.PLATFORM || coll.gameObject.tag == Tags.STICKYPLATFORM && coll.transform == currentPlatform.transform)
        {
			this.gameObject.transform.parent = null;
        }
    }

	void OnControllerColliderHit(ControllerColliderHit hit){ // works kinda but there is no Exit equivalent
		if (hit.gameObject.tag == Tags.STICKYPLATFORM) {
			currentPlatform = hit.transform;
			this.gameObject.transform.parent = hit.transform;
		}
	}
}
