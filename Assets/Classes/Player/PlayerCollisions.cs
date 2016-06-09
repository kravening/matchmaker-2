using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == Tags.FALLINGPLATFORM)
        {
            FallingPlatform fallingPlatform = coll.gameObject.GetComponent<FallingPlatform>();
            fallingPlatform.Fall();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
    }

    void OnTriggerStay(Collider other)
    {
		if (other.tag == Tags.PLATFORM || other.tag == Tags.STICKYPLATFORM)
        {
            transform.parent = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
		if (other.tag == Tags.PLATFORM || other.tag == Tags.STICKYPLATFORM)
        {
            transform.parent = null;
        }
        
        
    }
}
