using UnityEngine;
using System.Collections;

public class CalculateDistance : MonoBehaviour {

	public float CheckDistance(Transform _target)
    {
			return Vector3.Distance(_target.position, transform.position);
    }
}
