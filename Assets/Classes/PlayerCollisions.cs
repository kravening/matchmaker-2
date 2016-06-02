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
        if (other.tag == "Platform")
        {
            Debug.Log("parented");
            transform.parent = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform")
        {
            transform.parent = null;
        }
        
        
    }
}
