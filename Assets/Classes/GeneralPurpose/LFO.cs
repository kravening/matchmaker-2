using UnityEngine;
using System.Collections;
public enum LFO_Shape
{
	triangle,
	noise,
}
public class LFO : MonoBehaviour { // an LFO is a low frequency modulator used as an modulation source in music production. use this to "animate" between two different values

	//Behaviour Modifiers
	[SerializeField]private LFO_Shape shape; // sets the behaviour of the LFO
	[SerializeField]private float  _LFO_speed  = 1;          // the speed of the LFO (higher is faster, 0 = nothing aka useless)
	[SerializeField]private float  _LFO_depth  = 1;          // the max/min value of the LFO
	[SerializeField]private float  _LFO_offset = 0;          // the starting point of the LFO
	[SerializeField]private int    _LFO_ID     = 0;          // just an identification for the LFO, if you want to get to a certain LFO i guess, might be useless?
	[SerializeField]private bool   _LFO_Lerp   = true;       // a bool for enabling/disabling lerp
	[SerializeField]private float  _LerpSpeed  = .2f;        // speed of the lerp
	[SerializeField]private bool   _biPolar    = true;		 // allows the depth to go in the minus (aka 2 poles a minus and positive)
	protected float  _currentValue;                          // holds current "depth" of the LFO
	protected float  _timeStamp;                             // for time reasons
	protected float  _target;                                // for storing a target value (incase you want to lerp)


	public float currentValue      {get{return _currentValue;}} //get currentValue (basicly your modulation source);
	public int LFO_ID              {get{return _LFO_ID; }}      //get LFO ID

	protected virtual void Start(){
		Mathf.Clamp (_LFO_offset,-_LFO_depth,_LFO_depth);
		_currentValue = _LFO_offset;
		_timeStamp = Time.time;
	}
	void Update () {
		AddedBehaviour ();
	}

	protected void triangleOSC(){
		if (_biPolar) {
			if (_currentValue >= _LFO_depth || _currentValue <= -_LFO_depth) {
				Mathf.Clamp (_currentValue, -_LFO_depth, _LFO_depth); // Clam before changing currentvalue is important
				_LFO_speed = -_LFO_speed;
			}
		} else {
			if (_currentValue >= _LFO_depth || _currentValue <= 0) {
				Mathf.Clamp (_currentValue, -_LFO_depth, _LFO_depth); // Clam before changing currentvalue is important
				_LFO_speed = -_LFO_speed;
			}
		}
		_target = IncrementValue (_currentValue); // normal oscilation
		setValue ();
	}

	protected void NoiseOSC(){ // it really isn't an noise OSC, but it's makeshift noise if you put lfo speed on 0 :P
		if (Time.time >= _timeStamp + _LFO_speed) {
			_timeStamp = Time.time;
			_target = RandomDepthValue ();
		}
		setValue ();
	}

	protected void setValue(){
		if (_LFO_Lerp) {
			_currentValue = Mathf.Lerp (_currentValue,_target,_LerpSpeed);
		}else{
			_currentValue = _target;
		}
	}

	protected virtual void AddedBehaviour(){// container for extra behaviours
		if(shape == LFO_Shape.triangle){
			triangleOSC ();
			Debug.Log ("ayy");
		}
		else if (shape == LFO_Shape.noise) {
			NoiseOSC ();
			Debug.Log ("ayy");
		}
		// do some additional things here
	}
		

	protected float IncrementValue(float input){
		return input += _LFO_speed * Time.deltaTime;
	}

	protected float RandomDepthValue(){
		if (_biPolar) {
			return Random.Range (-_LFO_depth, _LFO_depth);
		} else {
			return Random.Range (0f, _LFO_depth);
		}
	}
}
