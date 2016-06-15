using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	[SerializeField]private Animator _animator;
	public void SetBoolParameter(string parameterName ,bool state){
		_animator.SetBool (parameterName, state);
	}
	public void SetFloatParameter(string parameterName, float value){
		_animator.SetFloat(parameterName, value);
	}
}
