using UnityEngine;
using System.Collections;

public class PlayerCollector : MonoBehaviour {
    [SerializeField]private Collectible _collectableManager;
    [SerializeField]private float       _delayTimer;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Collectable")
        {
            StartCoroutine(PickupRoutine());
        }
    }

    IEnumerator PickupRoutine()
    {
        _collectableManager.AddCollectable();
        yield return new WaitForSeconds(_delayTimer);
        
        //Destroy(collectible);
    }
}
