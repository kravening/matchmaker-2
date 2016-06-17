using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {
    [SerializeField]private float       _acceleration;
    [SerializeField]private float       _resetTime;
    [SerializeField]private float       _baseSpeed;
                    private float       _currentSpeed;
                    private Animator    _platformAnim;
                    private Vector3     _startPos;
                    private bool        _falling;

	void Start () 
    {
        _startPos = this.transform.position;
        _platformAnim = this.GetComponent<Animator>();
	}
	
    public void StartFall()
    {
        StartCoroutine(FallRoutine());
        Fall();
    }

    void Update()
    {
        Fall();
    }

    void Fall()
    {
        if (_falling == true)
        {
            transform.Translate(new Vector3(0, -10, 0) * _acceleration * Time.deltaTime);
        }
    }

    IEnumerator FallRoutine()
    {
        //_platformAnim.Play("Rumble");
        Debug.Log("Falling");
        yield return new WaitForSeconds(0.5f);
        _falling = true;
        //_platformAnim.Play("Idle");
        StartCoroutine(Reset());        
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(_resetTime);
        _falling = false;
        transform.position = _startPos;
    }
}
