using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {
	private Vector3 _collidingObjectPos;
	
    void Update(){
		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, Vector3.down, out hitInfo, 1f) && _collidingObjectPos != hitInfo.transform.position) { // makeshift onCollisionEnter
			_collidingObjectPos = hitInfo.transform.position;
			if(hitInfo.transform.tag == Tags.FALLINGPLATFORM){
				FallingPlatform fallingPlatform = hitInfo.transform.GetComponent<FallingPlatform> ();
				fallingPlatform.StartFall();
			}
		}else{
			_collidingObjectPos = new Vector3(0,0,0);
		}

	}
	void OnCollisionEnter(Collision coll)
    {
		Debug.Log (coll.gameObject.tag);
		if (coll.gameObject.tag == Tags.FALLINGPLATFORM)
        {
			Debug.Log ("AAAAAAAAAAAAAAAAAAAAA");
			FallingPlatform fallingPlatform = coll.gameObject.GetComponent<FallingPlatform>();
			fallingPlatform.StartFall();
        }
    }
}
