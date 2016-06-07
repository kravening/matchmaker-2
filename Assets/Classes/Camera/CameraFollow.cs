using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Transform _target;

	// Use this for initialization
	void Start () {
        _target = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FollowTarget()
    {

    }
}
