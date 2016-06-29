using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolState : MonoBehaviour , IEnemyState {

    [SerializeField]private List<Transform> _waypoints;
    [SerializeField]private float           _speed;
                    private int             _index;
                    private Transform       _target;
	// Use this for initialization
	void Start () {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
	        _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Patrol();
	}

    public void StateUpdate()
    {

    }

    public void ToPatrol() { }

    void Patrol()
    {
        if (_index == _waypoints.Count)
        {
            _index = 0;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_index].position, _speed * Time.deltaTime);
        }

        if (transform.position == _waypoints[_index].position)
        {
            if (_index < _waypoints.Count)
            {
                _index++;
            }
        }
    }

    public void ToChase()
    {

    }
}
