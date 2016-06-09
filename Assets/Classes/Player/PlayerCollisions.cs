using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "FallingPlatform")
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
		if (other.tag == "Platform" || other.tag == "Sticky_Platform")
        {
            transform.parent = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
		if (other.tag == "Platform" || other.tag == "Sticky_Platform")
        {
            transform.parent = null;
        }
        
        
    }
}
