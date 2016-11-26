using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
	{

    [SerializeField]private Text countText;
	private int _count;
	private int _nrOfTotalCollectables;
	private int _nrOfCollectedItems;

	void Start () 
	{
		GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
		_nrOfTotalCollectables = collectables.Length;
		_nrOfCollectedItems = 0;
		_count = 0;
		UpdateUI();
	}
	public void AddCollectable()
	{
		_nrOfCollectedItems++;
		Debug.Log ("You have " + _nrOfCollectedItems + " of the " + _nrOfTotalCollectables);
		_count += 1;
        Debug.Log("Score is " + _count);
		UpdateUI();

	}
	void UpdateUI()
	{
        Debug.Log("Score is " + _count.ToString());
		countText.text = "Score:" + _count.ToString ();
	}
}
