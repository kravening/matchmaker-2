using UnityEngine;
using System.Collections;

public class LFO_ParticleSize : LFO {
	[SerializeField]ParticleSystem _particleSystem;

	int particleCount;

	ParticleSystem.Particle[] _particles;

	private float[] _particlesSizes; //an array with wich allows for different Depths for each particle

	private float[] _particlesOffset; //an array with wich allows for different Offsets for each particle

	private float[] _particlesLFOSpeed; 

	protected override void Start ()
	{
		base.Start ();
	}
	protected override void AddedBehaviour(){
		base.AddedBehaviour ();
		if (particleCount != _particleSystem.particleCount) {
			_particles = new ParticleSystem.Particle[_particleSystem.particleCount];
		}

		
		/*
		float[] holdSizes = _particlesSizes;     //change sizes in these arrays then place back in original after editing
		float[] holdSpeeds = _particlesLFOSpeed;

		_particlesSizes = new float[_particleSystem.particleCount]; // saves the current sizes of particles

		if(_particlesLFOSpeed != null)
			holdSpeeds = _particlesLFOSpeed;
		
		_particlesLFOSpeed = new float[_particleSystem.particleCount];

		for (int i = 0; i < _particlesSizes.Length; i++) {
			if(holdSizes != null && i <= holdSizes.Length){
				_particlesSizes[i] = holdSizes[i]; //transfers particles sizes
			}

			if (_particlesSizes [i] == 0 && i <= _particlesSizes.Length) { // any new particles that may have been added gets a new random size assigned
				_particlesSizes [i] = RandomDepthValue ();
			}

			_particlesSizes [i] = IncrementValue (_particlesSizes [i], _particlesLFOSpeed [i]);
			//change particle size here
			// use previous for target :)
		}

		for (int i = 0; i < _particlesLFOSpeed.Length; i++) {

			if(holdSpeeds != null && i <= holdSpeeds.Length){
				_particlesLFOSpeed[i] = holdSpeeds[i]; // transfers lfo speed
			}

			if (_particlesLFOSpeed [i] == 0 && holdSpeeds.Length <= _particlesLFOSpeed.Length) {
				if (Random.Range (0, 1) == 1) {
					_particlesLFOSpeed[i] = _LFO_speed;
				} else {
					_particlesLFOSpeed[i] = -_LFO_speed;
				}
			}
			if (_particlesSizes[i] >= _LFO_depth || _particlesSizes[i] <= 0) {
				Mathf.Clamp (_particlesSizes[i], 0,_LFO_depth);
				_particlesLFOSpeed [i] = -_particlesLFOSpeed [i];
			}
		}
		*/
		_particleSystem.GetParticles (_particles); //Gets all paricles and stores them in _particles[]

		for (int i = 0; i < _particles.Length; i++) {
			_particles [i].size = currentValue; // override particles size with the lfo value.
		}
		_particleSystem.SetParticles (_particles, _particles.Length); // set particles
	}
}
