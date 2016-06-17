using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoints : MonoBehaviour {

    [SerializeField]private List<Transform> _checkpoints;
    private Transform _activeCheckpoint;
    private int _index;

	void Update () {
        CompareTransform();
	}

    public void CompareTransform()
    {
        for (int i = 0; i < _checkpoints.Count; i++)
        {
            if (Vector3.Distance(transform.position, _checkpoints[i].position) <= 10)
            {
                _activeCheckpoint = _checkpoints[i];
                Debug.Log("Checkpoint " + _checkpoints[i]);
            }
        }
    }

    public void GoToCheckpoint() //Makes player go to respawn point
    {
        transform.position = _activeCheckpoint.position;
    }
}
