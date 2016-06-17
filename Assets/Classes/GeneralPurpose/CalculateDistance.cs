using UnityEngine;
using System.Collections;

public class CalculateDistance : MonoBehaviour {

	public float CheckDistance(Transform _target)
    {
		Debug.Log (Vector3.Distance (_target.position, transform.position));
			return Vector3.Distance(_target.position, transform.position);
    }
}
