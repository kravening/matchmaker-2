using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveObject : MonoBehaviour {

    [SerializeField]private List<Transform> _waypoints;
    [SerializeField]private float           _defaultSpeed;
    [SerializeField]private float           _speed;
                    private int             _index;
                    private float           _distance;

	void Update () 
    {
        Move();
	}

    void Move()
    {
        Deccelarate(_waypoints[_index]);

        if (_index == _waypoints.Count)
        {
            _index = 0;
            _speed = _defaultSpeed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_index].position, _speed * Time.deltaTime);
        }

        if (transform.position == _waypoints[_index].position)
        {
            if (_index == _waypoints.Count)
            {
                _index = 0;
            }
        }

        _speed = _defaultSpeed;
    }

    void Deccelarate(Transform target)
    {
        _distance = Vector3.Distance(target.position, transform.position);
        if (_distance < 1)
        {
            _speed = _defaultSpeed * _distance;
        }

        if (_distance < 0.01f)
        {
            _index++;
        }
    }
}
