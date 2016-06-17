using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
	{
	public Text countText;
	private int _count;
	private int _nrOfTotalCollectables;
	private int _nrOfCollectedItems;

	void Start () 
	{
		GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
		_nrOfTotalCollectables = collectables.Length;
		_nrOfCollectedItems = 0;
		Debug.Log (_nrOfTotalCollectables);
		_count = 0;
		UpdateUI();
	}
	public void AddCollectable()
	{
		_nrOfCollectedItems++;
		Debug.Log ("You have " + _nrOfCollectedItems + " of the " + _nrOfTotalCollectables);
		_count = _count ++;
		UpdateUI();

	}
	void UpdateUI()
	{
		countText.text = "Score:" + _count.ToString ();
	}
}
