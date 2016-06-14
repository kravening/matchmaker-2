using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour {
    
    [SerializeField]private float _maxHealth;
                    private float _currentHealth;



	// Use this for initialization
	void Start () {
        _currentHealth = _maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
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
