using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolState : IEnemyState {

    private readonly StatePatternEnemy enemy;
    [SerializeField]private List<Transform> _waypoints;

    public void ChangeState()
    {

    }
    public void ToPatrol() 
    {
 
    }
    public void ToAlert()
    {

    }
    public void ToFollowPlayer()
    {

    }
}
