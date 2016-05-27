using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
    private CalculateDistance _calculateDistance;
    private bool _canTeleport;
    private Transform _player;
    [SerializeField]private Transform _teleportTo;
    // Use this for initialization
	
    void Start () {
        _player = GameObject.Find("Player").transform;
        _calculateDistance = GetComponent<CalculateDistance>();        
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(_calculateDistance.Distance + "it works");
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
        if (_calculateDistance.Distance < 1)
        {
            //enable UI element
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
        yield return new WaitForSeconds(2.5f);
        if (_canTeleport)
        {
            _player.position = _teleportTo.position;
            _canTeleport = false;
        }
    }
}
