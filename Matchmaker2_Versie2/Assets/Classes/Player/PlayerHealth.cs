using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour {
    
    [SerializeField]private float _maxHealth;
                    private float _currentHealth;

	void Start () {
        _currentHealth = _maxHealth;
	}

    public void DecreaseHealth()
    {
        if (_currentHealth > 0)
        {
            _currentHealth--;
        }
        else if (_currentHealth <= 0)
        {
            //Game over screen
        }
    }
}
