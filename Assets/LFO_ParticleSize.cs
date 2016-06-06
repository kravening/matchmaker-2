using UnityEngine;
using System.Collections;

public class LFO_ParticleSize : LFO {
	[SerializeField]ParticleSystem _particleSystem;

	ParticleSystem.Particle[] _particles;

	private int particleCount;

	protected override void Start ()
	{
		base.Start ();
		//ParticleSystem.GetParticles (_particles[]);
	}
	protected override void AddedBehaviour(){
		if (particleCount != _particleSystem.particleCount) { // only update count if needed
			_particles = new ParticleSystem.Particle[_particleSystem.particleCount];
		}

		_particleSystem.GetParticles (_particles); //Gets all paricles and stores them in _particles[]

		for (int i = 0; i < _particles.Length; i++) {
			_particles [i].size = currentValue + _particleSystem.startSize; // override particles size with the lfo value.
		}
		_particleSystem.SetParticles (_particles, _particles.Length); // set particles
	}
}
