using UnityEngine;
using System.Collections;

public class LFO : MonoBehaviour { // an LFO is a low frequency modulator used as an modulation source in music production. use this to "animate" between two different values

	//Behaviour Modifiers
	[SerializeField]private string _LFO_shape  = "triangle"; //TODO: Change to dropdown menu
	[SerializeField]private float  _LFO_speed  = 1;          // the speed of the LFO (higher is faster, 0 = nothing aka useless)
	[SerializeField]private float  _LFO_depth  = 1;          // the max/min value of the LFO
	[SerializeField]private float  _LFO_offset = 0;          // the starting point of the LFO
	[SerializeField]private int    _LFO_ID     = 0;          // just an identification for the LFO, if you want to get to a certain LFO i guess, might be useless?

	protected float  _currentValue;                            // holds current "depth" of the LFO
	protected float  _timeStamp;

	public float currentValue      {get{return _currentValue;}} //get currentValue (basicly your modulation source);
	public int LFO_ID              {get{return _LFO_ID; }}      //get LFO ID

	protected virtual void Start(){
		Mathf.Clamp (_LFO_offset,-_LFO_depth,_LFO_depth);
		_currentValue = _LFO_offset;
		_timeStamp = Time.time;
	}
	void Update () {
		Oscillate ();
	}

	protected virtual void Oscillate(){
		if (_currentValue >= _LFO_depth || _currentValue <= -_LFO_depth) { // 
			Mathf.Clamp (_currentValue, -_LFO_depth, _LFO_depth); // Clam before changing currentvalue is important
			_LFO_speed = -_LFO_speed;
		}
		/*if (_LFO_shape == "randomStep") {
			if (Time.time >= _timeStamp + _LFO_speed) {
				_timeStamp = Time.time;
				_currentValue = RandomDepthValue (); // randomStepLFO
			}
		}*/
		_currentValue = IncrementValue (_currentValue); // normal oscilation
		AddedBehaviour ();
	}

	protected virtual void AddedBehaviour(){// container for extra behaviours
		// do some additional things here
	}
		

	protected virtual float IncrementValue(float input){
		return input += _LFO_speed * Time.deltaTime;
	}

	protected virtual float RandomDepthValue(){
		return Random.Range (-_LFO_depth, _LFO_depth);
	}
}
