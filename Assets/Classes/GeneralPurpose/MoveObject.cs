using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveObject : MonoBehaviour {
    [SerializeField]private List<Transform> _objects;
    [SerializeField]private float _speed;
    private int _index;

	void Update () {
        Move();
	}

    void Move()
    {
        if (_index == _objects.Count)
        {
            _index = 0;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _objects[_index].position, _speed * Time.deltaTime);
        }
        if (transform.position == _objects[_index].position)
        {
            _index++;
        }
    }
}
