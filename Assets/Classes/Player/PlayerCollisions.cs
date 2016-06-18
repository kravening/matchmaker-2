using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {
	private Vector3 _collidingObjectPos;
	
    void Update(){
		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, Vector3.down, out hitInfo, 3f) && _collidingObjectPos != hitInfo.transform.position) { // makeshift onCollisionEnter
			_collidingObjectPos = hitInfo.transform.position;
			if(hitInfo.transform.tag == Tags.FALLINGPLATFORM){
				FallingPlatform fallingPlatform = hitInfo.transform.GetComponent<FallingPlatform> ();
				fallingPlatform.StartFall();
			}
		}else{
			_collidingObjectPos = new Vector3(0,0,0);
		}
	}
}
