using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

    [SerializeField]private Collectible _collectibleManager;
    [SerializeField]private PlayerRespawn _playerRespawn;
    private Vector3 _collidingObjectPos;
	
    void Update(){
        RaycastCollision();
	}

    void RaycastCollision()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 3f) && _collidingObjectPos != hitInfo.transform.position)
        {
            _collidingObjectPos = hitInfo.transform.position;
            if (hitInfo.transform.tag == Tags.FALLINGPLATFORM)
            {
                FallingPlatform fallingPlatform = hitInfo.transform.GetComponent<FallingPlatform>();
                fallingPlatform.StartFall();
            }
            else if (hitInfo.transform.tag == Tags.STICKYPLATFORM)
            {
                transform.parent = hitInfo.transform;
                return;
            }
        }
        else
        {
            _collidingObjectPos = new Vector3(0, 0, 0);
            transform.parent = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Collectable")
        {
            _collectibleManager.AddCollectable();
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Abyss")
        {
            _playerRespawn.Respawn();
        }
    }
}
