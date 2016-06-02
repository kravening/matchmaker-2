using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveObject : MonoBehaviour {
    [SerializeField]private List<Transform> _objects;
    [SerializeField]private float           _defaultSpeed;
    [SerializeField]private float           _speed;
                    private int             _index;
                    //private Transform _target;
                    private float _distance;

	void Update () {
        Move();
	}

    void Move()
    {
        //_target = _objects[_index];
        Deccelarate(_objects[_index]);

        if (_index == _objects.Count)
        {
            _index = 0;
            _speed = _defaultSpeed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _objects[_index].position, _speed * Time.deltaTime);
        }

        if (transform.position == _objects[_index].position)
        {
            /*if (_index < _objects.Count)
            {
                _index++;
            }*/

            if (_index == _objects.Count)
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
