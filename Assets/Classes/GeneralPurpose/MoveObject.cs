using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveObject : MonoBehaviour {
    [SerializeField]private List<Transform> _objects;
    [SerializeField]private float           _defaultSpeed;
                    private float           _speed;
                    private int             _index;
                    private Transform       _target;
                    private float _distance;

	void Update () {
        Move();
	}

    void Move()
    {
        _target = _objects[_index];

        Debug.Log("going to" + _objects[_index]);

        if (_index == _objects.Count)
        {
            _index = 0;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _objects[_index].position, _defaultSpeed * Time.deltaTime);
        }
        if (transform.position == _objects[_index].position)
        {
            _index++;
        }
    }

    void Deccelarate()
    {
        _distance = Vector3.Distance(_target.transform.position, transform.position);
    }
}
