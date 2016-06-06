using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

    [SerializeField]private float _rotateX;
    [SerializeField]private float _rotateY;
    [SerializeField]private float _rotateZ;

	void Update () 
    {
        RotatePlatform();
	}

    void RotatePlatform()
    {
        transform.Rotate(new Vector3(_rotateX, _rotateY, _rotateZ)* Time.deltaTime);
    }
}
