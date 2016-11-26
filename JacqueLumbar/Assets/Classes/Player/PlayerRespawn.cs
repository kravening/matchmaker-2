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
        //Respawn();
	}

    public void Respawn()
    {
        _playerHealth.DecreaseHealth();
        _checkpoints.GoToCheckpoint();        
    }
}
