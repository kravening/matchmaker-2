using UnityEngine;
using System.Collections;

public class PlayerCollector : MonoBehaviour {
    [SerializeField]private Collectible _collectableManager;
    [SerializeField]private float       _delayTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Collectable")
        {
            StartCoroutine(PickupRoutine(other.gameObject));
        }
    }

    IEnumerator PickupRoutine(GameObject collectible)
    {
        yield return new WaitForSeconds(_delayTimer);
        _collectableManager.AddCollectable();
        Destroy(collectible);
    }
}
