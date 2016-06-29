using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatePatternEnemy : MonoBehaviour {

    [SerializeField]private float           _searchTurnSpeed;
    [SerializeField]private float           _searchDuration;
    [SerializeField]private float           _sightRange;
    [SerializeField]private List<Transform> _waypoints;
    [SerializeField]private Transform _eyes;
    


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
