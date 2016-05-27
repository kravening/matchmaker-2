using UnityEngine;
using System.Collections;

public interface IEnemyState {

    void ChangeState();
    void ToPatrol();
    void ToAlert();
    void ToFollowPlayer();
}
