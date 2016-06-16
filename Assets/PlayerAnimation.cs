using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	private Animator _animator;
	void Start(){
		_animator = GetComponent<Animator> ();
	}

	public void SetBoolParameter(string parameterName ,bool state){
		_animator.SetBool (parameterName, state);
	}
	public void SetFloatParameter(string parameterName, float value){
		_animator.SetFloat(parameterName, value);
	}

	public bool GetParameter(string parameterName){
		return _animator.GetBool (parameterName);

	}
}
