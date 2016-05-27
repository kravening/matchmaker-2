using UnityEngine;
using System.Collections;

public class CalculateDistance : MonoBehaviour {
    [SerializeField]private Transform _otherObject;
    private float _distance;
    public float Distance
    {
        get { return _distance; }
        //set{_distance = Vector3.Distance(_otherObject.position,transform.position);}
    }

	void Update () {
        CheckDistance();
	}

    void CheckDistance()
    {
        if (_otherObject)
        {
            _distance = Vector3.Distance(_otherObject.position, transform.position);
            Debug.Log(_distance);
        }
    }
}
