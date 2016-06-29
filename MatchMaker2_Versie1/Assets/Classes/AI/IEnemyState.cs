using UnityEngine;
using System.Collections;

public interface IEnemyState{

    void StateUpdate();
    void ToPatrol();
    void ToChase();
}
