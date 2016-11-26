using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Author : Jordi Prummel

public class LoadScene : MonoBehaviour {
	[SerializeField]private string  _sceneName;
    [SerializeField]private int     _delay;

	public void ChangeLevel()
	{
        StartCoroutine(LoadLevel());
	}

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(_sceneName);
    }
}