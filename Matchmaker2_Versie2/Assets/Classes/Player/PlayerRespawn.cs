using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

    [SerializeField]private float _delayTimer;
                    private PlayerHealth _playerHealth;
                    private Checkpoints _checkpoints;
	// Use this for initialization
	void Start () {
        _playerHealth = GetComponent<PlayerHealth>();
        _checkpoints = GetComponent<Checkpoints>();
	}
	
	// Update is called once per frame
	void Update () {
        Respawn();
	}

    void Respawn()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 1f))
        {
            if (hitInfo.collider.tag == Tags.ABYSS)
            {
                StartCoroutine(DeathTimer());
            }
        }
    }

    IEnumerator DeathTimer()
    {
        Debug.Log("Respawning");
        //Spawn particle
        yield return new WaitForSeconds(_delayTimer);
        Debug.Log("Respawned");
        _playerHealth.DecreaseHealth();
        _checkpoints.GoToCheckpoint();        
    }
}
