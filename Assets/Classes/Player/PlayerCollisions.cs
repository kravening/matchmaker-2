using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {
	private Transform currentPlatform;
    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == Tags.FALLINGPLATFORM)
        {
			FallingPlatform fallingPlatform = other.gameObject.GetComponent<FallingPlatform>();
			fallingPlatform.StartFall();
        }
		if (other.gameObject.tag == Tags.PLATFORM || other.gameObject.tag == Tags.STICKYPLATFORM)
		{
			currentPlatform = other.transform;
			this.gameObject.transform.parent = other.transform;
		}
    }

	void OnCollisionExit(Collision other)
    {
		if (other.gameObject.tag == Tags.PLATFORM || other.gameObject.tag == Tags.STICKYPLATFORM && other.transform == currentPlatform.transform)
        {
			this.gameObject.transform.parent = null;
        }
    }
}
