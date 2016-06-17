using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoints : MonoBehaviour {

    [SerializeField]private List<Transform> _checkpoints;
    private Transform _activeCheckpoint;
    private int _index;

    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void CompareTransform(Transform checkpoint)
    {
        for (int i = 0; i < _checkpoints.Count; i++)
        {
            if (checkpoint == _checkpoints[i])
            {
                _activeCheckpoint = _checkpoints[i];
                Debug.Log("Active checkpoint : " + _activeCheckpoint);
                return;
            }
        }
    }

    public void GoToCheckpoint()
    {
        transform.position = _activeCheckpoint.position;
    }

    void ActivateCheckpoints()
    {
        _activeCheckpoint = _checkpoints[_index];
    }
}
