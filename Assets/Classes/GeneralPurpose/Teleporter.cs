using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
    [SerializeField]private Transform           _teleportTo;
                    private CalculateDistance   _calculateDistance;
                    private bool                _canTeleport;
                    private Transform           _player;
    
    // Use this for initialization
	
    void Start () 
    {
        _player             = GameObject.Find("Player").transform;
        _calculateDistance  = GetComponent<CalculateDistance>();        
	}
	
	// Update is called once per frame
	void Update () 
    {
        EnableTeleport();
        Teleport();
	}

    void Teleport()
    {
        if (_canTeleport)
        {
            StartCoroutine(Teleporting());
        }
    }

    void EnableTeleport()
    {
        if (_calculateDistance.Distance < 0.4f)
        {
            _canTeleport = true;
        }
        else
        {
            _canTeleport = false;
        }
    }

    IEnumerator Teleporting()
    {
        //spawn particle
        yield return new WaitForSeconds(1.5f);
        if (_canTeleport)
        {
            _player.position = _teleportTo.position;
            _canTeleport = false;
        }
    }
}
