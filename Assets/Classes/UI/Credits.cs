using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Credits : MonoBehaviour {
    [SerializeField]private Text _creditsText;
                    private Animator _creditsAnim;

	void Start () {

        _creditsAnim = GetComponent<Animator>();
        StartCoroutine(CreditsUI());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator CreditsUI()
    {
        //text fade in
        _creditsAnim.Play("FadeInText");
        yield return new WaitForSeconds(5);
        //text fade out
        _creditsAnim.Play("FadeOutText");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
