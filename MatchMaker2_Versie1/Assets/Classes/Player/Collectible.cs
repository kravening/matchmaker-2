using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
	{
	public Text countText;
	private int count;
	private int nrOfTotalCollectables;
	private int nrOfCollectedItems;

	void Start () 
	{
		GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
		nrOfTotalCollectables = collectables.Length;
		nrOfCollectedItems = 0;
		Debug.Log (nrOfTotalCollectables);
		count = 0;
		UpdateUI();
	}
	public void AddCollectable()
	{
		nrOfCollectedItems++;
		Debug.Log ("You have " + nrOfCollectedItems + " of the " + nrOfTotalCollectables);
		count = count + 1;
		UpdateUI();

	}
	void UpdateUI()
	{
		countText.text = "Score:" + count.ToString ();
	}
}
