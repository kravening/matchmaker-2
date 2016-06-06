using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {
    [SerializeField]private float       _speed;
                    private Animator    _platformAnim;

	void Start () 
    {
        _platformAnim = this.GetComponent<Animator>();
	}
	
    public void Fall()
    {
        StartCoroutine(FallRoutine());
    }

    IEnumerator FallRoutine()
    {
        _platformAnim.Play("Rumble");
        Debug.Log("Falling");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Fell");
        transform.Translate(new Vector3(0, -_speed, 0) * Time.deltaTime);
    }
}
