using UnityEngine;
using System.Collections;

public class LFO_ParticleSize : LFO {
	[SerializeField]ParticleSystem _particleSystem;

	ParticleSystem.Particle[] _particles;

	private int particleCount;

	private float[] _particlesDepth; //an array with wich allows for different Depths for each particle
	private float[] _particlesOffset; //an array with wich allows for different Offsets for each particle


	protected override void Start ()
	{
		base.Start ();
		//ParticleSystem.GetParticles (_particles[]);
	}
	protected override void AddedBehaviour(){
		base.AddedBehaviour ();
		if (particleCount != _particleSystem.particleCount) { // only update count if needed
			_particles = new ParticleSystem.Particle[_particleSystem.particleCount];
			_particlesOffset = new float[_particleSystem.particleCount];
		}

		_particleSystem.GetParticles (_particles); //Gets all paricles and stores them in _particles[]

		for (int i = 0; i < _particles.Length; i++) {
			_particles [i].size = currentValue; // override particles size with the lfo value.
		}
		_particleSystem.SetParticles (_particles, _particles.Length); // set particles
	}
}
